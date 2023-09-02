# SP_PhoneBookProject
 SP PhoneBook Project Semester 1


https://docs.google.com/document/d/1UxO5yLDaXUyjj1R220plTAkSURPtGie2SguLVHmbMKc/edit?usp=sharing


# Input Validation Project

## Table of Contents

1. [Overview](#overview)
2. [Description](#description)
3. [How It Works](#how-it-works)
4. [Achievements and Benefits](#achievements-and-benefits)
5. [How It Helps](#how-it-helps)
6. [Advantages and Disadvantages](#advantages-and-disadvantages)

## Overview

The "Input Validation Project" is a software application developed as part of the Secure Programming course at The University of Texas at Arlington. This project demonstrates the significance of rigorous input validation in maintaining data integrity and security.

## Description

This project focuses on creating a robust and secure system for managing phonebook entries. It offers four primary functionalities:

1. **Retrieve Phonebook Entries (/phoneBook/list):** This API retrieves all phonebook records and displays them in the response body.

2. **Add Phonebook Entry (/phoneBook/Add):** Users can add phonebook entries through this API, and the application ensures that the provided data is valid and conforms to predefined patterns.

3. **Delete Entry by Name (/phoneBook/deleteByName):** This API deletes a phonebook entry based on the provided name. It validates the input and ensures that only valid data is processed.

4. **Delete Entry by Number (/phoneBook/deleteByNumber):** Similar to the previous API, this endpoint deletes an entry based on the provided phone number, following strict input validation rules.

## How It Works

The project's codebase follows a structured approach:

- **Program.cs:** This serves as the entry point of the application. It handles API requests and initiates the workflow.

- **PhoneBookController.cs:** Responsible for routing API requests to the appropriate methods, this controller conducts initial data validation using model attributes.

- **_phoneBookService:** An interface defining the core operations for managing phonebook entries.

- **DictionaryPhoneBookService.cs:** An implementation of the _phoneBookService interface that interacts with an SQLite database to store and retrieve phonebook entries. It handles database operations and additional validation.

## Achievements and Benefits

The project attains several critical objectives:

- **Input Validation:** Rigorous input validation is at the core of this project. It ensures that all data entered into the phonebook system adheres to predefined rules and patterns, maintaining data quality.

- **Error Handling:** Custom validation attributes, such as "NameValidationAttribute" and "PhoneMaskAttribute," are used to catch and handle invalid data inputs. This results in effective error handling and improved user experience.

- **Logging:** The application incorporates NLog for logging relevant information and potential errors. Logging is essential for monitoring and debugging.

- **Structured API:** The API provides clear and structured endpoints for listing, adding, and deleting phonebook entries. This structure simplifies interaction for users and other applications.

## How It Helps

- **Data Quality:** By enforcing strict validation rules, the project ensures that the phonebook data remains accurate and free from inconsistencies, reducing the chances of incorrect or incomplete entries.

- **Security:** Input validation is a crucial component of application security. By rejecting malicious or invalid inputs, the project enhances the security of the phonebook system, mitigating potential threats.

- **User Experience:** Effective error handling and clear validation messages improve the user experience. Users receive prompt feedback when providing invalid data, reducing frustration and data entry errors.

- **Logging and Debugging:** The integration of NLog facilitates effective debugging and monitoring of the application, helping developers identify and resolve issues quickly.

## Advantages and Disadvantages

### Advantages

- **Customization:** The project uses highly customizable regular expressions for validation, allowing it to adapt to specific data format requirements.

- **Separation of Concerns:** Validation logic is separated from the core application code, enhancing code maintainability and flexibility.

- **Strict Validation:** Regular expressions enforce strict validation rules, ensuring that only valid data is processed, which contributes to data quality.

### Disadvantages

- **Complexity:** Writing and maintaining complex regular expressions can be challenging and error-prone.

- **Performance Impact:** Depending on the complexity of regular expressions and data volume, there may be a performance impact on the application.

- **Readability:** Regular expressions can be difficult to read and understand, potentially making code comprehension more challenging for developers.

In summary, the "Input Validation Project" is a robust and secure phonebook management system that showcases the importance of input validation in maintaining data integrity and security. While it leverages the power of regular expressions for validation, careful design and testing are essential to balance strict validation with system performance.
