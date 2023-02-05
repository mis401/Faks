#include "Heap.h"

Heap::Heap() {
	min = nullptr;
	root = nullptr;
	numberOfNodes = 0;
}

void Heap::insert(int data) {
	Node* newNode = new Node(data);

	newNode->next = 
}