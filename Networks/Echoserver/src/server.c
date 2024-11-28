#include "sock.h"

void req_proc(char *req, char *resp)
{
   char *sntnc = req;
   char *end = req + strlen(req);
   int resplen = 0;

   while (sntnc < end) {
      char *punct = strpbrk(sntnc, ".!?");

      if (punct == NULL) {
         sprintf(resp + resplen, "%s", sntnc);
         resplen += strlen(sntnc);
         break;
      }

      int sntnclen = punct - sntnc + 1;
      int lttrcnt = 0;

      for (char *ch = sntnc; ch <= punct; ch++)
         if (isalpha(*ch))
            lttrcnt++;

      sprintf(resp + resplen, "%.*s (%d)\n", sntnclen, sntnc, lttrcnt);
      resplen += sntnclen + 5;
      sntnc = punct + 1;
   }

   resp[resplen - 1] = '\0';
}

int main() {
   int ssock = socket(AF_INET, SOCK_STREAM, 0);

   if (ssock == -1)
       return err_catch("Unable to create socket\n", ssock);

   struct sockaddr_in addr = {
      .sin_family = AF_INET,
      .sin_port = htons(PORT),
      .sin_addr.s_addr = htonl(INADDR_ANY)
   };

   if (bind(ssock, (const struct sockaddr *)&addr, sizeof(addr)) == -1)
       return err_catch("Unable to bind\n", ssock);

   printf("Server started at %s, port %d\n",
      inet_ntoa(addr.sin_addr), htons(addr.sin_port));

   if (listen(ssock, 10) == -1)
      return err_catch("Unable to listen\n", ssock);
   
   struct sockaddr_in from;
   unsigned int fromsize = sizeof(from);
   int csock = accept(ssock, (struct sockaddr *)&from, &fromsize);

   if (csock == -1)
      return err_catch("Unable to accept\n", ssock);

   printf("New connection accepted from %s, port %d\n",
      inet_ntoa(from.sin_addr), htons(from.sin_port));

   while (1) {
      const int reqlen = 1024;
      char req[reqlen];

      if (recv(csock, req, reqlen, 0) == -1)
        return err_catch("Unable to recv\n", ssock);

      printf("Data received\n");

      char resp[2048];
      req_proc(req, resp);
      int resplen = strlen(resp);

      printf("Sending response from server\n");

      if (send(csock, resp, resplen, 0) == -1)
         return err_catch("Unable to send\n", ssock);

      printf("Response sent\n\n");
      memset(resp, 0, resplen);
   }

   close(ssock);
   return 0;
} 