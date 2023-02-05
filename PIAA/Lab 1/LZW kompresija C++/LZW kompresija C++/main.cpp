#include <iostream>
#include <unordered_map>
#include <string>
#include <fstream>
using namespace std;


int main(int argc, char** argv) {



}



bool CompressVar(string srcFile, string destFile) {
	
	unordered_map<string, short>* map = new unordered_map<string, short>();

	ifstream source(srcFile);
	ofstream dest(destFile);

	for (int i = 0; i < 256; i++) {
		map->insert
	}

}
