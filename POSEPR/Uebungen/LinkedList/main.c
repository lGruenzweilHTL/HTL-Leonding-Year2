#include <stdio.h>
#include <stdlib.h>

struct Node {
    int data;
    struct Node *next;
};
struct Node *head = NULL;

void printList() {
    struct Node *curr;
    printf("\n");

    curr = head;

    if (head == NULL) {
        printf("The list is empty\n");
        return;
    }

    while (curr != NULL) {
        printf("%d\n", curr->data);
        curr = curr->next;
    }

    printf("\n");
}

void append(int value) {
    struct Node *newElement;
    struct Node *curr;
    curr = head;

    newElement = (struct Node*) malloc(sizeof(struct Node));

    newElement->data = value;
    newElement->next = NULL;

    // insert into list
    if (curr == NULL) {
        head = newElement;
    }
    else {
        // find end of list
        while (curr->next != NULL) {
            curr = curr->next;
        }

        curr->next = newElement;
    }
}

void deleteTail() {


    struct Node *curr;
    curr = head;

    if (curr != NULL) {
        if (curr->next == NULL) {
            head = NULL;
            free(curr);
        }
        else {
            while (curr->next->next != NULL) {
                curr = curr->next;
            }

            free(curr->next);
            curr->next = NULL;
        }
    }
}

int main(void) {
    char input = 'n';

    while (input != 'q') {
        printf("Enter action [(q)uit, (n)ew, r(emove), p(rint)]: ");
        scanf("%c", &input);
        getchar();

        switch (input) {
            case 'q': 
                return 0;
                break;
            case 'n':
                int n;
                printf("Enter number to append: ");
                scanf("%d", &n);
                getchar();
                append(n);
                break;
            case 'r':
                deleteTail();
                break;
            case 'p':
                printList();
                break;
        }
    }

    return 0;
}