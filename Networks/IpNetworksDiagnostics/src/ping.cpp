#define _WINSOCK_DEPRECATED_NO_WARNINGS
#include <winsock2.h>
#include <ws2tcpip.h>
#include <iphlpapi.h>
#include <icmpapi.h>
#include <iostream>
#include <string>
#pragma comment(lib, "iphlpapi.lib")
#pragma comment(lib, "ws2_32.lib")
using namespace std;


const int kBufferSize = 4;
const int kRequestCount = 4;
const char kTtl = 64;

void InitLibrary()
{
    WSAData wsaData;
    WSAStartup(MAKEWORD(2, 2), &wsaData);
}

SOCKET CreateSock()
{
    SOCKET sock = socket(AF_INET, SOCK_RAW, IPPROTO_ICMP);
    setsockopt(sock, IPPROTO_IP, IP_TTL, &kTtl, sizeof(kTtl));
    return sock;
}

bool ConvertHostName(string& hostname, string& host, IPAddr& ipaddr)
{
    hostent* host_entity = gethostbyname(hostname.c_str());
    if (host_entity == nullptr)
        return false; 

    in_addr* addr = (in_addr*)(host_entity->h_addr_list[0]);
    host = inet_ntoa(*addr);
    inet_pton(AF_INET, host.c_str(), &ipaddr);

    return true;
}

void MakeRequest(IPAddr ipaddr, string& host)
{
    int min_time = 1000, average_time = 0, max_time = 0;
    int sent_count = 0, received_count = 0;
    char* request_buffer = new char[kBufferSize];

    for (int i = 0; i < kBufferSize; i++)
        request_buffer[i] = rand();

    HANDLE icmp_handle = IcmpCreateFile();
    int reply_size = sizeof(ICMP_ECHO_REPLY) + kBufferSize;
    char* reply_buffer = new char[reply_size];

    for (int i = 0; i < kRequestCount; ++i)
    {
        int result = IcmpSendEcho(icmp_handle, ipaddr, request_buffer, kBufferSize, NULL, reply_buffer, reply_size, 1000);
        PICMP_ECHO_REPLY echo_reply = (PICMP_ECHO_REPLY)reply_buffer;
        sent_count++;

        if (result != 0)
        {
            struct in_addr reply_addr;
            reply_addr.S_un.S_addr = echo_reply->Address;

            cout << "Ответ от "
                << inet_ntoa(reply_addr)
                << ": число байт=" << kBufferSize + sizeof(ICMP_ECHO_REPLY)
                << " время=" << echo_reply->RoundTripTime
                << "мс"
                << " TTL=" << (int)echo_reply->Options.Ttl
                << endl;

            received_count++;

            if (echo_reply->RoundTripTime < min_time)
                min_time = echo_reply->RoundTripTime;

            else if (echo_reply->RoundTripTime > max_time)
                max_time = echo_reply->RoundTripTime;

            average_time += echo_reply->RoundTripTime;
        }
        else
        {
            cout << "Возникла ошибка, статус: " << echo_reply->Status << endl;
            return;
        }
    }

    delete[] request_buffer;
    delete[] reply_buffer;

    cout << endl;

    float loss = (float)(sent_count - received_count) / sent_count * 100;
    cout << "Статистика Ping для " << host << ":\n"
        << "    Пакетов: отправлено = " << sent_count
        << ", получено = " << received_count
        << ", потеряно = " << sent_count - received_count
        << "\n" << "    (" << loss << "% потерь)" << endl;

    cout << "Приблизительное время приема-передачи в мс:" << endl;
    cout << "    Минимальное = " << min_time << "мсек, "
        << "Максимальное = " << max_time << "мсек, "
        << "Среднее = " << (average_time / kRequestCount) << "мсек" << endl << endl;
}

int main() {
    setlocale(0, "");

    InitLibrary();
    SOCKET sock = CreateSock();

    while (true) {
        cout << "Введите имя узла (или 'exit' для выхода): ";
        string hostname;
        string host;
        IPAddr ipaddr;
        cin >> hostname;

        if (hostname == "exit")
            break;

        if (!ConvertHostName(hostname, host, ipaddr)) 
        {
            cout << "При проверке связи не удалось обнаружить узел "
                << hostname << "." << endl
                << "Проверьте имя узла и повторите попытку.\n" << endl;
            continue;
        }

        cout << "\n\nОбмен пакетами с " << hostname << " [" << host << "] "
            << "с " << kBufferSize + sizeof(ICMP_ECHO_REPLY)
            << " байтами данных:\n";

        MakeRequest(ipaddr, host);
        ipaddr = 0;
    }

    closesocket(sock);
    WSACleanup();

    cout << "Выход из программы." << endl;
    return 0;
}