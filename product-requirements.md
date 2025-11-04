# Product Requirements Document (PRD`)
## Title: Learning Management System (LMS)
### Author: Gideon Isa
### Date: October 08, 2025
### Version: 1.0
### Status: Draft

### Version | Date       | Author      | Change Description
### --------|------------|-------------|-------------------
### 1.0     | 2025-10-08 | Gideon Isa  | Initial draft

---

## 1. Overview
The Learning Management System (LMS) is a next-generation, immersive learning platform that integrates Virtual Reality (VR) technology with tradititional web-based educaton management.
It is designed to deliver an engaging, interactive, and scalble learning experience that supports both standard digital learning and immersive 3D environment accessed via VR headsets. It enables instructors to create and manage courses, students to enroll and complete lessons, and administrators to monitor performance and manage the ecosystem.

Through the platform, students can access lessons, complete interactive simulations, and participate in realistic learning scenarios using VR goggles, 
whole instructors and administrators can manage courses, track performance, analyze learning outcome through a web-based dashboard.

The LMS supports multi-modal learning - combining traditional video, document-based and assessment-driven modules with immersive VR-based content such as virtual labs, simulations, 
and experimental learninf sessions.

The goal is to enhance learner engagement and retention throught immersive experinences, while maintaining the reliability, analytics, and scalability of a modern enterprise-grade LMS.

## 2. Objectives
- Provide an intuitive and secure learning environment for students and instructors.
- Centrize course creation, content delivery, and learning progress tracking
- Support asynchronous (self-paced) leaning.
- Facilitate data-driven insights for instructors and admins through reporting and analytics.
- Ensure extensibility for integration with thrid-party tools

## 3. Key Stakeholders
| Role              |                 Responsiblity                                         |
| **Administrator** | Manage users, courses, roles, and system configurations.              |
| **Instructor**    | Create, publish, and manage courses, assignments, and assessments.    |
| **Student**       | Enroll in courses, consume content, take assessments, and view grades.|

## 4. Core Features

## 4.1 User Management
- User registration and authentication (JWT)
- Role-based access control (Admin, Instructor, Student).
- Profile management and password reset.


## 4.2 Course Management
- Create (draft), update, publish and archive courses
- Categorization by topic, skill level, and tags
- Course overview with thumbnail, description, and instructor details

## 4.3 Module & Lesson Management
- Courses divided into modules -> lessons.
- Lessons may contain video, document, or text content
- Track completion status and time spent

## 4.4 Enrollment
- Manual enrollment of students in a course by instructor or admin.
- Enrollment tracking with progress and completion status.
- Enrollment linites and prerequisities.

## 4.5 Assessment (Quizzes & Assignments)
- Quizzes: timed, auto-graded with multiple question types (MCQ).
- Assignments: instructor-reviewed submission (file or text).
- Feedback and grading support.

## 4.6 GradeBooK
- Aggregated scores from quizzes and assignments.  
- Weighted grading system per course.  
- Export grades as CSV/PDF.  
- Instructor and student grade views.

### 4.7 Certificates
- Auto-generate certificate upon course completion.  
- Include student name, course title, completion date, and instructor signature.  
- Downloadable and shareable link.

### 4.9 Notifications
- System notifications for deadlines, announcements, new content, and grades.  
- Email and in-app delivery options.

## 5. Non-Functional Requirements

| Category | Description |
|-----------|-------------|
| **Performance** | System should handle 1,000+ concurrent learners per tenant. |
| **Security** | JWT-based authentication, password hashing, role-based access control. |
| **Scalability** | Clean architecture, with cloud deployment readiness. |


## 6. System Architecture Overview

- **Frontend:** .  
- **Backend:** ASP.NET Core Web API.  
- **Database:** PostgreSQL or SQL Server.  
- **Authentication:** Identity + JWT tokens.  
- **File Storage:** .  
- **Deployment:** Docker containers, CI/CD pipeline.  


## 7. Success Metrics
- Course completion rate ? 70%  
- Average user satisfaction score ? 4/5  
- Instructor adoption rate ? 80% of targeted users  
- 0 major security incidents in first 12 months

## 8. Milestones & Deliverables

| Milestone  | Deliverable | Target Date |
|------------|-------------|--------------|
| Phase 1 | Requirements & Architecture Design | [Date] |
| Phase 2 | Core API (User, Course, Enrollment) | [Date] |
| Phase 3 | Content & Assessment Modules | [Date] |
| Phase 4 | GradeBook & Certificate Module | [Date] |
| Phase 5 | Notifications & Discussions | [Date] |
| Phase 6 | QA, Security Review, and Launch | [Date] |
