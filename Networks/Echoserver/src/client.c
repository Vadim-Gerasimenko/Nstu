#include "sock.h"

int main() {
   int csock = socket(AF_INET, SOCK_STREAM, 0);

   if (csock == -1)
      return err_catch("Unable to create socket\n", csock);

   char ip[16];
   printf("Enter IP: ");
   fgets(ip, 16, stdin);

   struct sockaddr_in cin = {
   .sin_family = AF_INET,
   .sin_port = htons(PORT),
   .sin_addr.s_addr = inet_addr(ip)
   };

   if (connect(csock, (struct sockaddr *)&cin, sizeof(cin)) == -1)
      return err_catch("Unable to connect\n", csock);

   printf("Connection made sucessfully\n");

   while (1) {
      printf("Enter text to send request or '~' to shutdown server\n");

      int reqlen = 1024;
      char req[reqlen];
      fgets(req, reqlen, stdin);

      printf("Sending request from client\n");

      if (send(csock, req, reqlen, 0) == -1)
         err_catch("Unable to send\n", csock);

      int resplen = 2048; 
      char resp[resplen];
      int s;

      if ((s = recv(csock, resp, resplen, 0)) == -1)
         return err_catch("Unable to recv\n", csock);
   
      if (req[0] == '~') {
         puts(resp);
         break;
      }

      printf("\nResponse from server: %s\n", resp);

      memset(req, 0, strlen(req));
      memset(resp, 0, strlen(resp));
   }

   close(csock);
   return 0;
}