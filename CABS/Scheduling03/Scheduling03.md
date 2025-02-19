# Scheduling Exercises 03

## 1. List at least five scheduling algorithms in interactive Systems

- Round-Robin Scheduling
- Priority Scheduling
- Multiple Queues
- Shortest Process Next
- Guaranteed Scheduling

## 2. Processes in interactive systems need long CPU bursts and short I/O time. Right or wrong? Clarify in your own words.

Wrong, because interactive systems depend a lot on User input (aka. I/O). So interactive system have longer I/O time and short CPU bursts.

## 3. Explain the Round-Robin scheduling algorithm in your own words.

Each process gets assigned a quantum (amount of time to run).

If the process is blocked or has finished before its quantum has run out, a scheduling decision will be made.
If the process has not finished when its quantum is over, a scheduling decision will be made.

A short quantum means a lot of context switches, which wastes CPU resources.
A large quantum makes response time unsatifsactory.

## 4. One problem of priority scheduling is that high priority processes might run infinitely whereas others get no CPU at all. How can a high priority process be prevented from running infinitely? There are 2 possible alternatives.

1. The Scheduler decreases priority of the currently running process with each clock tick.
2. Each process gets a maximum quantum time it is allowed to run in one piece.

## 5. Priority scheduling. Explain the idea of multiple queues in your own words.

There are multiple priority classes.

Processes in the highest class are allowed to run **one quantum**.
Processes in the next-lowest class are allowed to run **two quanta**.
Processes in the next-lowest class are allowed to run **four quanta**.
And so on...

When a process has used up all its quanta, it is moved down one class.

**BUT** Processes in the higher classes go before the ones in the lower classes, preventing lower-classed processes from running too long at once.

## 6. Shortest process next: The following table shows processes of a system which are all in state ready. Which process will be selected next, supposed the scheduler works according to the *Shortest Process Next* strategy? Explain in your own words!

| Process name | 1st run | 2nd run | 3rd run | 4th run |
| ------------ | ------- | ------- | ------- | ------- |
| Process A | 50 msec | 150 msec | 300 msec | 85 msec |
| Process B | 300 msec | 150 msec | 85 msec | 50 msec |

### Process A

T0 = 50 msec
T1 = 150 msec
T2 = 300 msec
T3 = 85 msec

We don't need to calculate E1 and E2 for this example, we can jump straight to E3

E3 = T3/2 + T2/4 + T1/8 + T0/8 = 142,5 msec

### Process B

E3 = (50 / 2) + (85 / 4) + (150 / 8) + (300 / 8) = 102,5 msec

### Result

According to the *Shortest Process Next* strategy, **Process B** will be selected.

This makes sense, because the later runs matter more for the calculation and Process B already had the longer runs at the start.

## 7. Explain in own words how priorities can be realized with lottery scheduling. Give an example!

Lottery Scheduling works by giving each process a lottery ticket for CPU Time.
The Scheduler then draws a ticket and selects the process that is holding that ticket.

Priorities can be realised by giving the Processes with higher priority more tickets, increasing the chance that they're selected.

### Example (Client-Server-System)

- Client has some tickets and gets CPU to collect all data from user
- Upon all data is collected, client sends data to server and needs no more CPU
    - client will send all tickets to server
- Server has many tickets now
    - Gets much CPU time to process data from client
    - When finished send tickets back to client
- Client has CPU for further user input...

This takes advantage of the fact, that processes can send their tickets to other processes.
When the client doesn't need the CPU anymore, it sends all tickets to the server (effectively increasing its priority).

When the server is done processing the data, it send all its tickets to the client (increasing its priority).

## 8. Explain in own words the term Real-Time System. Give an example.

A *Real-Time-System* is a system that needs to guarantee a response within a specific timeframe (deadline).
A deadline must always be met, regardless of system load or other factors.

Real-Time-Systems are all about processing data quickly (The data is usually provided by external devices like sensors).

One example of a Real-Time-System would be the *Dynamic Cruise Control* in a Car. It gets data from the cars sensors and needs to process this data quick enough to be able to adjust the speed **in real time**. (You don't want the car to start slowing down when you've already crashed into the car in front of you)

## 9. Explain in own words the difference between a soft real-time system and a hard real-time system. Give examples.

### Hard real-time system

In a hard real-time system deadlines must be met **at all costs**.

An example for this would be an Aircraft control system. This system failing to provide a response could result in some serious damages.

### Soft real-time system

In a soft real-time system deadlines should still be met, but an **occasional miss** is not the end of the world.

An example of a soft real-time system is a weather station.