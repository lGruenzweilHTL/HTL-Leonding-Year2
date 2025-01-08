#include <stdio.h>

#define DAYS 3
#define HOURS 24

int Measurements[DAYS][HOURS] = {
    {5, 12, 18, 13, 7, 8, 15, 16, 14, 13, 5, 3, 8, 10, 12, 16, 17, 13, 10, 12, 16, 19, 18, 8},
    {6, 15, 16, 17, 14, 9, 10, 11, 13, 16, 12, 10, 15, 16, 17, 19, 18, 16, 14, 12, 13, 15, 14, 11},
    {5, 14, 17, 16, 11, 9, 8, 10, 11, 14, 16, 18, 19, 17, 14, 11, 9, 12, 13, 15, 17, 16, 19, 18}
};

char Output[20][HOURS];

void InitializeOutput() {
    // Fill the output matrix with spaces
    for (int y = 0; y < 20; y++) {
        for (int x = 0; x < HOURS; x++) {
            Output[y][x] = ' ';
        }
    }
}

void CreateBarChart() {
    for (int h = 0; h < HOURS; h++) {
        int value = Measurements[0][h]; // Measurement for the first day, hour h
        for (int y = 0; y < value; y++) {
            if (y < 20) {
                Output[19 - y][h] = '|'; // Vertical line for the bar
            }
        }
    }
}

void CreateStemChart() {
    for (int h = 0; h < HOURS; h++) {
        int value = Measurements[0][h]; // Measurement for the first day, hour h
        Output[19 - value][h] = '*'; // Stem for the measurement
    }
}

void CreateLineChart() {
    for (int t = 0; t < DAYS; t++) {
        for (int h = 0; h < HOURS; h++) {
            int value = Measurements[t][h];
            Output[19 - value][h] = '*'; // Line for the measurement
        }
    }
}

void PrintOutput() {
    for (int y = 0; y < 20; y++) {
        for (int x = 0; x < HOURS; x++) {
            printf("%c", Output[y][x]);
        }
        printf("\n");
    }
}

int main() {
    InitializeOutput();

    printf("Bar chart for the first day:\n");
    CreateBarChart();
    PrintOutput();

    InitializeOutput();
    printf("\nStem chart for the first day:\n");
    CreateStemChart();
    PrintOutput();

    InitializeOutput();
    printf("\nLine chart for all three days:\n");
    CreateLineChart();
    PrintOutput();

    return 0;
}
