#include <stdio.h>
#include <stdlib.h>

struct Node {
    int Data;
    struct Node *Next;
    struct Node *Previous;
};

struct Node *start;
struct Node *end;

void printList() {
    struct Node *curr = start;
    while (curr != NULL) {
        printf("%d ", curr->Data);
        curr = curr->Next;
    }

    printf("\n");
}

void insertStart(int data) {
    struct Node *newNode = (struct Node*) malloc(sizeof(struct Node*));
    newNode->Data = data;

    if (start == NULL) {
        start = newNode;
        end = newNode;
    }
    else {
        newNode->Next = start;
        start->Previous = newNode;
        start = newNode;
    }
}
void insertEnd(int data) {
    struct Node *newNode = (struct Node*) malloc(sizeof(struct Node*));
    newNode->Data = data;

    if (end == NULL) {
        start = newNode;
        end = newNode;
    }
    else {
        end->Next = newNode;
        newNode->Previous = end;
        end = newNode;
    }
}

void deleteStart() {
    struct Node *help = start;
    if (start != NULL) {
        start = start->Next;
    }

    if (start == NULL) {
        end = NULL;
    }
    else {
        start->Previous = NULL;
    }

    free(help);
}
void deleteEnd() {
    if (end != NULL) {
        end = end->Previous;
    }

    if (end == NULL) {
        start = NULL;
    }
}

int main(void) {
    char inp = '0';

    printf("Operations:\ni: Insert start\nd: Delete start\ne: Insert end\nf: Delete end\nq: Quit\np: Print\n\n");

    while (inp != 'q') {
        printf("Input operation: [i | d | e | f | p]: ");
        scanf(" %c", &inp);
        int data;

        switch (inp) {
            case 'i':
                printf("Enter data: ");
                scanf("%d", &data);

                insertStart(data);
                break;
            case 'e':
                printf("Enter data: ");
                scanf("%d", &data);

                insertEnd(data);
                break;
            case 'd':
                deleteStart();
                break;
            case 'f':
                deleteEnd();
                break;
                case 'p':
                printList();
                break;
        }
    }
}