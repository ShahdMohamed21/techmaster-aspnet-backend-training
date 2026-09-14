using TrainingCenter.Api.Entities.Students;
using TrainingCenter.Api.Entities.Instructors;
using TrainingCenter.Api.Entities.Tracks;
using TrainingCenter.Api.Entities.Enrollments;

namespace TrainingCenter.Api.Data.Seed;

public static class SeedData
{
    public static Student[] Students =>
    [
        new Student
        {
            Id = 5,
            FullName = "Shahd Mohamed",
            Email = "shahd@example.com",
            CreatedAt = new DateTime(2026, 1, 1),
            IsActive = true
        },

        new Student
        {
            Id = 6,
            FullName = "Ahmed Ali",
            Email = "ahmed@example.com",
            CreatedAt = new DateTime(2026, 1, 2),
            IsActive = true
        },

        new Student
        {
            Id = 7,
            FullName = "Mariam Hassan",
            Email = "mariam@example.com",
            CreatedAt = new DateTime(2026, 1, 3),
            IsActive = true
        },

        new Student
        {
            Id = 8,
            FullName = "Omar Khaled",
            Email = "omar@example.com",
            CreatedAt = new DateTime(2026, 1, 4),
            IsActive = true
        },

        new Student
        {
            Id = 9,
            FullName = "Salma Ahmed",
            Email = "salma@example.com",
            CreatedAt = new DateTime(2026, 1, 5),
            IsActive = true
        }
    ];

    public static Instructor[] Instructors =>
    [
        new Instructor
        {
            Id = 5,
            FullName = "Ahmed Hassan"
        },

        new Instructor
        {
            Id = 6,
            FullName = "Sara Mohamed"
        }
    ];

    public static TrainingTrack[] TrainingTracks =>
    [
        new TrainingTrack
        {
            Id = 5,
            Name = "Backend Development",
            InstructorId = 5
        },

        new TrainingTrack
        {
            Id = 6,
            Name = "Frontend Development",
            InstructorId = 6
        },

        new TrainingTrack
        {
            Id = 7,
            Name = "AI Fundamentals",
            InstructorId = 5
        }
    ];

    public static Enrollment[] Enrollments =>
    [
        new Enrollment
        {
            Id = 8,
            StudentId = 5,
            TrainingTrackId = 5,
            Status = "Active",
            EnrollmentDate = new DateTime(2026, 2, 1)
        },

        new Enrollment
        {
            Id = 9,
            StudentId = 5,
            TrainingTrackId = 7,
            Status = "Active",
            EnrollmentDate = new DateTime(2026, 2, 2)
        },

        new Enrollment
        {
            Id = 10,
            StudentId = 6,
            TrainingTrackId = 5,
            Status = "Active",
            EnrollmentDate = new DateTime(2026, 2, 3)
        },

        new Enrollment
        {
            Id = 11,
            StudentId = 7,
            TrainingTrackId = 6,
            Status = "Active",
            EnrollmentDate = new DateTime(2026, 2, 4)
        },

        new Enrollment
        {
            Id = 12,
            StudentId = 8,
            TrainingTrackId = 7,
            Status = "Active",
            EnrollmentDate = new DateTime(2026, 2, 5)
        }
    ];
}