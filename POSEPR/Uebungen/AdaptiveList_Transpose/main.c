#include <stdio.h>
#include <stdlib.h>

struct Node {
    struct Node *Next;
    struct Node *Previous;
    int Data;
};

struct Node *start = NULL;
struct Node *end = NULL;

void display() {
    struct Node *curr = start;
    printf("\nList contents:\n");
    while (curr != NULL) {
        printf("%d\n", curr->Data);
        curr = curr->Next;
    }
    printf("\n");
}

void append(int data) {
    struct Node *newNode = malloc(sizeof(struct Node));
    if (!newNode) {
        printf("Memory allocation failed\n");
        exit(1);
    }

    newNode->Data = data;
    newNode->Next = NULL;
    newNode->Previous = NULL;

    if (start == NULL) {
        start = end = newNode;
    } else {
        start->Previous = newNode;
        newNode->Next = start;
        start = newNode;
    }
}

void delete() {
    if (end == NULL) {
        return;
    }

    struct Node *help = end;

    if (end->Previous != NULL) {
        end = end->Previous;
        end->Next = NULL;
    } else {
        // Only one node
        start = end = NULL;
    }

    free(help);
}

void transpose(struct Node *node) {
    if (!node || node == start) return;

    struct Node *prev = node->Previous, *next = node->Next;

    // Remove prev from the list
    if (prev->Previous) prev->Previous->Next = node;
    node->Previous = prev->Previous;

    // Insert node in prev's position
    prev->Next = next;
    if (next) next->Previous = prev;

    node->Next = prev;
    prev->Previous = node;

    // Update start or end if needed
    if (start == prev) start = node;
    if (end == node) end = prev;
}


int search(int data) {
    struct Node *curr = start;

    while (curr != NULL) {
        if (curr->Data == data) {
            transpose(curr);
            return 1;
        }
        curr = curr->Next;
    }

    return 0;
}

int main(void) {
    append(1);
    append(2);
    append(3);
    append(4);
    display();

    delete();
    display();

    if (search(2)) {
        printf("Found 2 and moved forward\n");
    } else {
        printf("2 not found\n");
    }

    display();

    return 0;
}
