 Driving & Vehicle License Department (DVLD) Management System
 
 Overview
This project is a comprehensive **Driving & Vehicle License Department (DVLD) Management System** designed to manage the issuance and renewal of driving licenses, handle various license-related services, and track the status of licenses and requests. The system supports multiple user types, from administrators managing licenses to applicants submitting requests.

The system is enhanced by an API Layer, which provides robust communication between the front-end applications, external systems, and the backend, ensuring smooth data flow and scalability.

 Key Features
1. License Issuance Services:
   - First-time license issuance.
   - License renewal.
   - Issuing replacement for lost or damaged licenses.
   - Unblocking a suspended license.
   - Issuing international licenses.

2. License Classes:
   The system supports several categories of licenses based on the vehicle type and driver qualifications:
   - Motorcycle licenses (small and heavy).
   - Regular car licenses.
   - Commercial licenses** (taxis, limousines).
   - Agricultural vehicle licenses.
   - Bus and heavy vehicle licenses.

3. **Request Processing**:
   Users can submit applications for services by providing:
   - National ID or applicant details.
   - Request type (e.g., new issuance, renewal).
   - Required payments.
   - Documentation and eligibility checks for the specific license category.

4. **User and License Data Management**:
   The system maintains detailed user profiles and ensures the uniqueness of user records:
   - Personal information including name, date of birth, nationality, contact details, and photo.
   - License details, including the type, validity period, and associated fees.

5. Examinations and Testing:
   Applicants must pass a series of tests (medical, theoretical, and practical driving exams) before obtaining a license. The system tracks:
   - Exam dates and results.
   - The ability to reschedule exams for failed attempts.

6. System Administration:
   - User management with roles and permissions.
   - Management of services, requests, and their fees.
   - Monitoring license statuses (active, suspended, etc.).
   - Ability to manage the different classes of licenses and update their requirements.

 API Layer

The API Layer is a key component that allows external systems to interact with the DVLD Management System. It is built using .NET Core and exposes endpoints to handle requests related to:

- License Issuance: API endpoints for creating and managing license issuance requests, including license class selection, applicant verification, and fee processing.
- User Management: Endpoints to create, update, and retrieve user profiles.
- License Status: API to track the current status of licenses, renewals, and testing results.
- Examinations: API endpoints for scheduling, updating, and retrieving examination details and results.
- Administrative Services: Endpoints for managing system users, requests, fees, and license categories.

 

 Technology Stack
- Frontend: User-friendly interface integrated with the API.
- Backend: A .NET Core-based API Layer managing business logic and data flow.
- Database: Stores applicant records, license data, test results, and service fees.





