# 3 Routers Protocol

## Networks
This 3-Router configuration consists of 4 Networks.
1. The 10.1 network
2. The 10.2 network
3. The 10.3 network
4. The 10.4 connection network

## Configuration
The **10.4 connection network** consists of a switch, where the 3 Routers are connected.

The other 3 Networks have a switch connected to the Router and 2 Hosts connected to the switch.

## IP-Setup
### Router 1
IP1 (in 10.4): 10.4.0.1
IP2 (in 10.1): 10.1.0.1

### Router 2
IP1 (in 10.4): 10.4.0.2
IP2 (in 10.2): 10.2.0.1

### Router 3
IP1 (in 10.4): 10.4.0.3
IP2 (in 10.3): 10.3.0.1

### Hosts
Each host gets and IP-Adress in their network with the Routers Adress as the Standard-Gateway.

## Routing Table
## Router 1
### Entry 1
**Network**: 10.2.0.0
**Mask**: 255.255.0.0
**Next Hop**: 10.4.0.2

### Entry 2
**Network**: 10.3.0.0
**Mask**: 255.255.0.0
**Next Hop**: 10.4.0.3

## Routers 2 & 3
Make 2 Table entries for each router with the network you want to route to (use the examples from Router 1)

