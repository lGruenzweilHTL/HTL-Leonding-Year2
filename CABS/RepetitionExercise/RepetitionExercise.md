# Repetition Exercise

1. Explain the purpose of the address bus.
    - The address bus is used when the CPU wants to communicate with the RAM. The address that the CPU wants to access is communicated using the address bus.
2. The data bus of a computer system is undirectional. True or false. Explain in your own words.
    - False, the data bus can be used in both directions (read and write). When reading from the RAM, the RAM puts the data onto the data bus for the CPU to access. When writing to the RAM, the CPU puts the data onto the data bus for the RAM to access.
3. Explain the purpose of a control unit (CU) in a computer system in your own words.
    - The Control unit is used to Decode instructions into commands and sends them to the ALU to execute.
4. Explain the "Von-Neumann-Cycle".
    - The [Von-Neumann-Cycle](https://de.wikipedia.org/wiki/Von-Neumann-Zyklus) is a 5-Step cycle to execute commands. 
    The cycle works like this:
    1. Fetch
        - Fetch the command from memory
    2. Decode
        - Decode the commands into machine instructions
        - Increment the Program counter
    3. Fetch operands
        - Fetch any additional operands the command might need
    4. Execute
        - Execute the current command
    5. Write back
        - Write any results of the command back to memory
5. Is there any relation between the with of the address bus and the maximum number of RAM that can be addressed
Explain in your own words.
    - The maximum number of addresses that can be addressed in the RAM is 2^n, where n is the width of the address bus. For example a 32-bit address bus can only address 4GB of RAM while a 64-Bit address bus can address 18 Exabytes.
6. What is a register of a CPU? Give two examples of special registers of a CPU.
    - A register is a small but fast memory storage inside of the CPU. Registers are usually just a few Bytes in size. Some special registers are the [PC (Program Counter)](https://de.wikipedia.org/wiki/Befehlsz%C3%A4hler) and the [PSW (Program Status Word)](https://en.wikipedia.org/wiki/Program_status_word).
7. Explain in your own words: What is meant by the term "DMA" in a computer system?
    - The [DMA (Direct Memory Access)](https://de.wikipedia.org/wiki/Direct_Memory_Access) is responsible for memory access without the CPU. This allows several devices like peripherals and the network interface card to directly access the memory. One big benefit of this is the fact that the CPU can focus on other tasks while the DMA fetches data from memory.
8. Describe in your own words the therm "bus width" (in the context of computer science)
    - The width of the bus (in bits) is the number of parallel wires a bus has. The wider the bus is, the more distinct memory addresses can be saved on the bus. For example a 32-bit bus can address 4 Gigabytes of RAM in parallel.
9. Is it possible to increment the program counter (PC) after the execution of the just loaded instruction (i.e. increment the PC not immediately after the instruction fetch)? Yes or now. Explain in your own words
    - It is generally possible but it's not recommended, as it requires a few extra steps to make it work. The biggest problem with this approach is the `jmp` (jump) command. This changed the Program Counter to the specified address. Incrementing the PC after this process would result in the Program Counter being set the wrong value.