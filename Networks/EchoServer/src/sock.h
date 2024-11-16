#include <stdio.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netdb.h>
#include <netinet/in.h>
#include <arpa/inet.h>
#include <string.h>
#include <ctype.h>
#include <unistd.h>

#define PORT 2009

int err_catch(char *msg, int sock)
{
   printf("%s", msg);
   close(sock);
   return 0;
}