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

void enqueue(int data) {
    struct Node *newElement;
    newElement = (struct Node*) malloc(sizeof(struct Node));
    newElement->Data = data;
    newElement->Next = head;

    head = newElement;
}

int dequeue() {
    if (head == NULL) {
        printf("Deque is empty\n");
        return -1; // Indicate empty deque
    }

    int value;

    // Only one node in the deque
    if (head->Next == NULL) {
        value = head->Data;
        free(head);
        head = NULL;
        return value;
    }

    // More than one node
    struct Node* temp = head;
    while (temp->Next->Next != NULL) {
        temp = temp->Next;
    }

    value = temp->Next->Data;
    free(temp->Next);
    temp->Next = NULL;

    return value;
}


int top() {
    struct Node *temp;
    temp = head;

    if (head == NULL) {
        return -1;
    }

    while (temp->Next != NULL) {
        temp = temp->Next;
    }
    return temp->Data;
}

int queueEmpty() {
    return (head == NULL);
}

void clearQueue() {
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
    enqueue(10);
    enqueue(20);
    enqueue(30);
    enqueue(40);

    printf("Queue before clear:\n");
    display();

    printf("\nTop element: %d\n", top());
    printf("Popping element: %d\n", dequeue());
    printf("Queue after pop:\n");
    display();

    printf("Queue empty state: %d\n", queueEmpty());

    clearQueue();
    printf("\nQueue after clear:\n");
    display();

    return 0;
}
