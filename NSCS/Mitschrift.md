#  ISO-OSI Referenzmodell

7: Application L. (Anwendungsschicht)
6: Presentation L. (Darstellungsschicht)
5: Session L. (Sitzungsschicht)
4: Transport L. (Transportschicht)
3: Network L. (Vermittlungsschicht)
2: Datalink Layer (Sicherungsschicht)
1: Physical Layer (Bitübertragungsschicht)

Merksatz: **P**lease **d**o **n**ot **t**hrow **S**alami **P**izza **a**way.

# (DoD) TCP/IP-Modell

7+6+5: Application L.
4: Transport L.
3: Internet L.
2+1: Network Access Layer (Netzzugangsschicht)


# IPs

IP:         7  |.0.0.?
Subnetmask: 255|.0.0.0
---------NetID | HostID

Router: 7.0.0.254
Broadcast: 7.255.255.255

# ISO-OSI Addressing

| Layer | Addressing | Kopplungsgeräte | PDU (Protocol Data Unit) |
| ----- | --------- | ---------------- | ------------------------ |
| 7 Application | | | Daten |
| 6 Presentation | | | Daten |
| 5 Session | | | Daten |
| 4 Transport | Port A. (2 bytes) | | Segment |
| 3 Network | IPv4-Adr (4 bytes) | Router | Packet |
| 2 Datalink | MAC-Adr. (6 bytes) | Switch | Frame |
| 1 Physical | | | Bits |

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

## Bsp 1
IP-Adr: 120.96.1.200
Subnetmask: 255.0.0.0
Net ID: 120
Host ID: 96.1.200
Net-Adr: 120.0.0.0
Broadcast Adr: 120.255.255.255

## Bsp 2
172.96.1.200
255.255.0.0
172.96
1.200
172.96.0.0
120.96.255.255

## Bsp 3
192.96.1.200
255.255.255.0
193.96.1
200
193.96.1.0
193.96.1.255