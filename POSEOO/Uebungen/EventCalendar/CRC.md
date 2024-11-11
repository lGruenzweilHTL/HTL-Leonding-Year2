# CRC-Analysis of EventCalendar Program

### 1.

#### Class
Person

#### Responsibilities
Hold Data describing a Person (Name, EMail, Phone number), displaying a person as string (long and short), importing multiple people from a csv file

#### Collaborations
None

### 2.

#### Class
Event

#### Responsibilities
Hold Data describing an Event (Title, Date, Invitor, Participants, ...), methods to interact with the event (invite/remove people, sort participants), displaying an event as string, factory method for importing multiple events from a csv file

#### Collaborations
Person

### 3.
#### Class
Program

#### Responsibilities
Import mode:
Import data from files and construct events

Demo mode:
Create demo event and print it to the console

#### Collaborations
Person, Event
