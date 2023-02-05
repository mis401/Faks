#include <iostream>
#include <winsock.h>
#include <string>
#pragma comment(lib, "wsock32.lib");
#define BUF_SIZE 1024

using namespace std;

int main(int argc, char** argv) {
	WSAData wsa;
	SOCKET listenSocket;
	if (WSAStartup(0x0202, &wsa) != 0) {
		return -1;
	}
	u_short port;
	cout << "Port?:" << endl;
	cin >> port;

	if ((listenSocket = socket(AF_INET, SOCK_STREAM, 0)) == INVALID_SOCKET) {
		cout << "Greska u soketu";
		return -2;
	}
	sockaddr_in server;
	server.sin_family = AF_INET;
	server.sin_addr.s_addr = INADDR_ANY;
	server.sin_port = htons(port);

	if (bind(listenSocket, (sockaddr*)&server, sizeof(server)) == SOCKET_ERROR) {
		cout << "Bind failed";
		return -3;
	}

	if (listen(listenSocket, 3) < 0) {
		cout << "listen full";
		return -4;
	}

	while (true) {

		sockaddr_in client;
		int cLen = sizeof(sockaddr_in);
		SOCKET clientSocket;
		if ((clientSocket = accept(listenSocket, (sockaddr*)&client, &cLen)) < 0) {
			cout << "Accept fail";
			return -5;
		}
		char* buffer = new char[BUF_SIZE];
		//string buffer;
		int rcvMsgSize = recv(clientSocket, buffer, BUF_SIZE, 0);

		char* i = new char[BUF_SIZE];
		while (rcvMsgSize > 0) {
			cout << buffer << endl;
			i = strcpy(i, "Pokusaj 1");
			if (send(clientSocket, i, strlen(i)+1, 0) != strlen(i) + 1)
				return -10;
			cout << "sent attempt 1\n";
			if (send(clientSocket, buffer, rcvMsgSize, 0) != rcvMsgSize)
				return -10;
			cout << "sent message\n";

			i = strcpy(i, "Pokusaj 2");
			if (send(clientSocket, i, strlen(i)+1, 0) != strlen(i) + 1)
				return -10;
			cout << "sent attempt 2\n";
			if (send(clientSocket, buffer, rcvMsgSize, 0) != rcvMsgSize)
				return -10;
			cout << "sent message\n";

			if ((rcvMsgSize = recv(clientSocket, buffer, BUF_SIZE, 0)) < 0)
				return -11;
			
		}
		closesocket(clientSocket);
	}
	closesocket(listenSocket);
	WSACleanup();
	return 0;
}