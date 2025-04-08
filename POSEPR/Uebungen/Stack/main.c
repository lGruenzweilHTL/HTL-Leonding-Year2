#include <stdio.h>
#include <stdlib.h>

struct Node {
    int Data;
    struct Node *Next;
};

struct Node *head;

void display() {
    struct Node *curr = head;

    while (curr != NULL) {
        printf("%d\n", curr->Data);
        curr = curr->Next;
    }
}

void push(int data) {
    struct Node *newElement;
    newElement = (struct Node*) malloc(sizeof(struct Node));
    newElement->Data = data;
    newElement->Next = head;

    head = newElement;
}

int pop() {
    struct Node *help;
    help = head;
    int data = -1;
    if (head != NULL) {
        data = head->Data;
        head = head->Next;
        free(help);
    }

    return data;
}

int top() {
    if (head != NULL) {
        return head->Data;
    }

    return -1;
}

int stack_empty() {
    return (head == NULL);
}

void stack_clear() {
    struct Node* curr = head;
    struct Node* temp;
    while (curr != NULL) {
        temp = curr;
        curr = curr->Next;
        free(temp);  // Free the current node
    }
    head = NULL;  // Reset head to NULL after clearing the stack
}

int main() {
    // Demonstration program
    push(10);
    push(20);
    push(30);
    push(40);

    printf("Stack before clear:\n");
    display();

    printf("\nTop element: %d\n", top());
    printf("Popping element: %d\n", pop());
    printf("Stack after pop:\n");
    display();

    printf("Stack empty state: %d\n", stack_empty());

    stack_clear();
    printf("\nStack after clear:\n");
    display();

    return 0;
}
