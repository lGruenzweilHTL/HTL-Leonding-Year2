# IPs

IP:         7  |.0.0.?
Subnetmask: 255|.0.0.0
---------NetID | HostID

Router: 7.0.0.254
Broadcast: 7.255.255.255

# ISO-OSI Addressing

| ISO-OSI Layer | TCP/IP Layer | Addressing | Kopplungsgeräte | PDU (Protocol Data Unit) | Protokolle |
| ------------- | ------------ | ---------- | ---------------- | ----------------------- | ---------- |
| 7 Application | Application  |            |                  | Daten                   | HTTPS(s), DHCP, DNS, FTP, POP3, IMAP, SMTP |
| 6 Presentation | Application |            |                  | Daten                   |            |
| 5 Session      | Application |            |                  | Daten                   |            |
| 4 Transport    | Transport   | Port A. (2 bytes) |           | Segment                 | TCP, UDP   |
| 3 Network      | Internet    | IPv4-Adr (4 bytes) | Router   | Packet                  | ICMP       |
| 2 Datalink     | Network Access | MAC-Adr. (6 bytes) | Switch | Frame                  | Ethernet   |
| 1 Physical     | Network Access |         |                  | Bits                    |            |
**P**lease **d**o **n**ot **t**hrow **S**alami **P**izza **a**way.

## MAC-Address
6 bytes

3B -> Herstellerspezifisch
3B -> Fortlaufende Sequenz

No MAC-Address can be ambiguous

# IPv4-Address

| IPv4-Address | Net-ID | Host ID |
| ------------ | ------ | ------- |
| IP-Adr decimal | 192.168.10. | 1 |
| IP-Adr binary | 11000000.10101000.0001010. | 00000001 |
| Subnetmask d. | 255.255.255. | 0 |
| Subnetmask binary | 11111111.11111111.11111111. | 00000000 |

## Beispiele

| Example | IP Address    | Subnet Mask   | Net ID  | Host ID  | Net Address   | Broadcast Address   |
|---------|---------------|---------------|---------|----------|---------------|---------------------|
| Bsp 1   | 120.96.1.200  | 255.0.0.0     | 120     | 96.1.200 | 120.0.0.0     | 120.255.255.255     |
| Bsp 2   | 172.96.1.200  | 255.255.0.0   | 172.96  | 1.200    | 172.96.0.0    | 120.96.255.255      |
| Bsp 3   | 192.96.1.200  | 255.255.255.0 | 193.96.1| 200      | 193.96.1.0    | 193.96.1.255        |
| Bsp 4   | 192.168.10.1 and 192.168.1.3 | 255.255.0.0 | 192.168 | 10.1 bzw. 1.3 | 192.168.0.0 | 192.168.255.255 |