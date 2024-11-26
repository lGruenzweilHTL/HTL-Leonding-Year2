#include <stdio.h>
#include <stdlib.h>

void print(unsigned char reg){
    for (size_t i = 0; i < 8; i++) {
        if (reg & 128) {
            printf("x");
        }
        else {
            printf(" ");
        }
        reg <<= 1;
    }
    printf("\n");
}

void print_x_shape() {
    unsigned char l = 128; // 10000000
    unsigned char r = 1; // 00000001

    while (l > 0 && r < 129) {
        print(l | r);
        l >>= 1;
        r <<= 1;
    }
}
void print_checkerboard_pattern() {
    unsigned char row1 = 0b01010101;  // 01010101 pattern (alternating 0 and 1)
    unsigned char row2 = 0b10101010;  // 10101010 pattern (alternating 1 and 0)

    for (int i = 0; i < 4; i++) {
        print(row1);  // Print row with alternating 0 and 1
        print(row2);  // Print row with alternating 1 and 0
    }
}
void print_right_triangle() {
    for (unsigned char i = 1; i <= 8; i++) {
        unsigned char row = (1 << i) - 1;  // Set the first i bits to 1
        print(row);  // Print the row
    }
}

int main(int argc, char **argv) {
    print_x_shape();
    printf("\n\n");
    print_checkerboard_pattern();
    printf("\n\n");
    print_right_triangle();

    return 0;
}