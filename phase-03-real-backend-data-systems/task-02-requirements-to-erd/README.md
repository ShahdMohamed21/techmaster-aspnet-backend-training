Task 02 – Requirements to ERD
Overview

Designed an Entity Relationship Diagram (ERD) for the TechMaster Academy training management system based on the given business requirements.

Entities

The database contains five main entities:

Student – Stores student information.
Instructor – Stores instructor information.
TrainingTrack – Stores training track details and its main instructor.
Enrollment – Connects students with training tracks and stores enrollment progress.
Payment – Stores payment transactions related to enrollments.
Relationships
Student 1 → Many Enrollments
Instructor 1 → Many TrainingTracks
TrainingTrack 1 → Many Enrollments
Enrollment 1 → Many Payments

A student can enroll in multiple training tracks, and each training track can have multiple students through the Enrollment entity.

An enrollment can have multiple payments because students may pay in multiple installments.

Key Design Decisions
Primary keys uniquely identify each entity.
Foreign keys are used to maintain relationships between entities.
Enrollment acts as the bridge between Student and TrainingTrack.
Payment is linked to Enrollment rather than directly to Student or TrainingTrack.
Amount uses a decimal type for financial values.
System-generated dates use UTC.
Unique constraints are applied to fields such as Student Email, Instructor Email, and Training Track Code.
Business Questions Supported

The database design can answer questions such as:

Which students are enrolled in a specific track?
Which tracks have available seats?
Which enrollments are unpaid?
How much revenue did each track generate?
Which instructor has the highest workload?
Which students have active enrollments?
Which tracks start this month?
What is the payment history for an enrollment?
Which tracks are full?
How many enrollments exist by status?
Deliverables
ERD Diagram
Tables and fields
Primary and foreign keys
Relationship definitions
Business rules
Business questions supported by the database design
