# Services
### DHCP
**D**ynamic **H**ost **C**onfiguration **P**rotocol.

Verteilt IP-Adressen, Subnetmask und Default-Gateway. **DHCP verteilt DNS-Server**

### DNS
**D**omain **N**ame **S**ystem

### Webserver
`index.html` als Erstzugriff.

### HTTP
**H**yper**t**ext **T**ransfer **P**rotocol

### SMPT
Simple Mail Transfer Protocol
Senden von Mails.

### IMAP

Abrufen von Mails.
Mails werden vom Server **gespiegelt**.

### POP3
Post Office Protocol v3.
Abrufen von Mails.
Mails werden vom Server **heruntergeladen** (danach nicht mehr am server).

### FTP
**F**ile **T**ransfer **P**rotocol.

### ICMP
Internet Message Access Protocol.
**I**nternet **C**ontrol **M**essage **P**rotocol

## UDP
User Datagram Protocol.

## ARP
Address resolution Protocol.
Useful when needing the MAC-Adress from a known IP-Adress.

# IPs

IP:         7  |.0.0.?
Subnetmask: 255|.0.0.0

Router: 7.0.0.254
Broadcast: 7.255.255.255

# ISO-OSI Addressing

| ISO-OSI Layer | TCP/IP Layer | Addressing | Kopplungsgeräte | **PDU** (Protocol Data Unit) | Protokolle |
| ------------- | ------------ | ---------- | ---------------- | ----------------------- | ---------- |
| 7 Application | Application  |            |                  | Daten                   | HTTP(S), DHCP, DNS, FTP, POP3, IMAP, SMTP |
| 6 Presentation | Application |            |                  | Daten                   |            |
| 5 Session      | Application |            |                  | Daten                   |            |
| 4 Transport    | Transport   | Port A. (2 bytes) |           | Segment                 | TCP, UDP   |
| 3 Network      | Internet    | IPv4-Adr (4 bytes) | Router   | Packet                  | ICMP       |
| 2 Datalink     | Network Access | MAC-Adr. (6 bytes) | Switch | Frame                  | Ethernet   |
| 1 Physical     | Network Access |         |                  | Bits                    |            |
**P**lease **d**o **n**ot **t**hrow **S**alami **P**izza **a**way.

## MAC-Address
MAC = **M**edia **A**cess **C**ontrol.

6 bytes lang

3B -> Herstellerspezifisch

3B -> Vom Hersteller vergebene, einzigartige Sequenz

No MAC-Address can be ambiguous

# IPv4-Address

**I**nternet **P**rotocol

4 bytes

| IPv4-Address | Net-ID | Host ID |
| ------------ | ------ | ------- |
| IP-Adr decimal | 192.168.10. | 1 |
| IP-Adr binary | 11000000.10101000.0001010. | 00000001 |
| Subnetmask d. | 255.255.255. | 0 |
| Subnetmask binary | 11111111.11111111.11111111. | 00000000 |

## Beispiele
| Example | IP Address (CIDR) | Subnet Mask | Net ID | Host ID | Net Address   | Broadcast Address   |
|---------|---------------|---------------|---------|----------|---------------|---------------------|
| Bsp 1   | 120.96.1.200/8 | 255.0.0.0    | 120     | 96.1.200 | 120.0.0.0     | 120.255.255.255     |
| Bsp 2   | 172.96.1.200/16 | 255.255.0.0 | 172.96  | 1.200    | 172.96.0.0    | 120.96.255.255      |
| Bsp 3   | 192.96.1.200/24 | 255.255.255.0 | 193.96.1 | 200   | 193.96.1.0    | 193.96.1.255        |
| Bsp 4   | 192.168.10.1/16 and 192.168.1.3/16 | 255.255.0.0 | 192.168 | 10.1 bzw. 1.3 | 192.168.0.0 | 192.168.255.255 |

## CIDR Notation
Number of bits in Net ID.

Written like: A.B.C.D/n

Example: 192.168.0.0/**16** (possible CIDR numbers: **8, 16, 24**)

Min-IP Adr.: 192.168.0.1 (192.168.0.0 is reserved for Net Adr.)
Max-IP Ar.: 192.168.255.254 (192.168.255.255 is reserved for Broadcast Adr.)

# IPv6
128 bits (16 bytes)

Existiert nur, weil IPv4 der Adressraum ausging.

# Data Encapsulation
![](Tafelbild.jpeg)

## FCS
**F**rame **C**heck **S**equence