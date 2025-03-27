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

int lookup(int value) {
    int idx = -1;
    struct Node *curr = head;

    while (curr != NULL) {
        idx++;
        if (curr->Data == value) {
            return idx;
        }

        curr = curr->Next;
    }

    return -1;
}

int main(void) {
    head = NULL;

    insertStart(7);
    insertStart(9);
    insertStart(3);

    deleteStart();

    printList();

    insertStart(5);
    insertStart(8);
    insertStart(15);

    printf("7 at index: %d\n", lookup(7));
    printf("8 at index: %d\n", lookup(8));
}