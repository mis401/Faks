#include "Node.h"

Node::Node() {
	data = 0;
	next = nullptr;
	prev = nullptr;
	parent = nullptr;
	lmc = nullptr;
}

Node::Node(int d) {
	data = d;
	next = prev = parent = lmc = nullptr;
}

Node::~Node() {}














































































