#pragma once
#include "Node.h"
class Heap
{
private:
	Node* min;
	Node* root;
	int numberOfNodes;
	
public:
	Heap();
	~Heap();
	void insert(int data);
	int extractMin();
	void modify(int data);
	void remove(int data);
	void consolidate();

};

