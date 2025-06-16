#include <stdio.h>
#include <stdlib.h>

struct Node {
    struct Node *Next;
    struct Node *Previous;
    int Data;
    int Frequency;
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
    newNode->Frequency = 0;
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

void moveByFrequency(struct Node *node) {
    if (!node || node == start) return;

    struct Node *curr = node;
    while (curr->Previous && curr->Previous->Frequency < node->Frequency) {
        struct Node *prev = curr->Previous;
        struct Node *next = curr->Next;

        // Remove current from position
        if (prev->Previous)
            prev->Previous->Next = curr;
        curr->Previous = prev->Previous;

        prev->Next = next;
        if (next)
            next->Previous = prev;

        curr->Next = prev;
        prev->Previous = curr;

        // Update start and end if necessary
        if (start == prev)
            start = curr;
        if (end == curr)
            end = prev;
    }
}


int search(int data) {
    struct Node *curr = start;

    while (curr != NULL) {
        if (curr->Data == data) {
            curr->Frequency++;
            moveByFrequency(curr);
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

    printf("Looking for 2...\n");
    search(2);
    display();

    printf("Looking for 3...\n");
    search(3);
    display();

    printf("Looking for 2 again...\n");
    search(2);
    display();

    printf("Looking for 1...\n");
    search(1);
    display();

    printf("Looking for 2 again...\n");
    search(2);
    display();

    return 0;
}
