#  System Architecture Document 
## Title: Learning Management System (LMS)
### Author: Gideon Isa
### Date: October 08, 2025
### Version: 1.0
### Status: Draft

### Version | Date       | Author      | Change Description
### --------|------------|-------------|-------------------
### 1.0     | 2025-10-08 | Gideon Isa  | Initial draft

# System Architecture - LMS with VR Integration

## 1. Architectural Overview
The Learning Management System (LMS) is designed using the clean architecture s. The system is built to support high availability, fault tolerance, and seamless integration with third-party services that integrate traditional e-learning functionality with immersive VR experiences.

## 2. Core Architectural Style
| Attribute | Description |
|------------|--------------|
| **Type** | **Monolithic (Clean Architecture)** — A single deployable system internally divided into logical layers. |
| **Pattern** | **Clean Architecture / Onion Architecture** for separation of concerns and flexibility. |
| **Deployment** | Cloud-native environment using **Docker** containers and **CI/CD pipelines**. |
| **API** | **RESTful API**, extensible to **GraphQL** or **gRPC** for VR module communication. |
| **Framework** | **ASP.NET Core** (Backend) + **React** (Web Frontend) + **Unity 3D** (VR Client). |

## 3. System Components
The LMS backend follows the clean architecture structure with five primary layers:

### **3.1 Domain Layer**
- Core of the application
- Contains business logic, value objects, and domain entities
- Independent of external frameworks and technologies

- **Key Entities**:
  - User (Admin, Instructor, Student)
  -	Program/Leraning Path - contains multiple courses
  - Course -> contains multiple modules 
  - Module -> contains multiple lessons
  - Lesson -> contains content (video, document, VRExperience)
  - Enrollment -> links students to courses
  - Assessment -> quizzes, assignments
  - VRExperience -> metadata for VR content (e.g., 3D models, simulations
	


## 3.2 Application Layer
- Contains use cases  and application services
- Orchestrates interactions between the domain layer and external layers
- Uses interfaces to communicate with infrastructure services
- Handles business rules and workflows
- Contains CQRS pattern for command and query separation

## 3.3 Infrastructure Layer
- Handles external integrations and data access.
- Implements repositories, database contexts, and external service clients.

- **Components**:
	- Database: MSSQL Server
	- ORM: Entity Framework Core
	- File Storage: AWS S3
	- Cache: Redis
	- Authentication: JWT, Identity

## 4.0 Data Flow
- Instructor creates and publishes courses via web client; 
- students access courses via web or VR clients.
- Instructor enrolls students via web client;
- VR client request immersive content via REST API -> downloads VR scene/assets from storage;
- Progress and scores sync back to GradeBook via REST API or event-driven updates.
- Analytics data collected for user engagement and performance tracking for admin dashboards.

## 5.0 Integration Architecture


## 7.0 Deployment Architecture

## 8.0 Security Architecture
- Authentication: OAuth2.0 / JWT.
- Authorization: Role & Claim-based.
- Secure VR Communication: HTTPS + signed API tokens.
- Data Protection: Encryption (AES-256 at rest, TLS 1.3 in transit).

## 9.0 Summary
The LMS architecture combines clean architecture principles with modern cloud-native practices to deliver a robust, scalable, and extensible learning platform. 
The integration of VR technology enhances the learning experience, while the modular design ensures maintainability and future growth.

 