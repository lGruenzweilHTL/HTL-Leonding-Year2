#include <stdio.h>
#include <stdlib.h>

struct Node {
    int Data;
    struct Node *Next;
};

struct Node *head;

void printList() {
    struct Node *curr = head;

    while (curr != NULL) {
        printf("%d\n", curr->Data);
        curr = curr->Next;
    }
}

void insertStart(int data) {
    struct Node *newElement;
    newElement = (struct Node*) malloc(sizeof(struct Node));
    newElement->Data = data;
    newElement->Next = head;

    head = newElement;
}

void deleteStart() {
    struct Node *help;
    help = head;
    if (head != NULL) {
        head = head->Next;
        free(help);
    }
}

int main(void) {
    head = NULL;

    insertStart(5);
    insertStart(3);

    deleteStart();

    printList();
}