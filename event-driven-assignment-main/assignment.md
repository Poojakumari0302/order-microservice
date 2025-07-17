# Overview

You are provided with a sample Order microservice that follows **Domain-Driven Design (DDD)** and **Event-Driven Architecture (EDA)**. The microservice contains a basic **Order Aggregate**.

Your task is to analyze a new **user story**, refine it by asking clarifying questions, design a solution based on the requirements, and then break it down into **technical deliverables**.

## User Story

> **As a customer, I want to be able to cancel an order within 15 minutes of placing it, so that I can avoid unwanted charges.**

### Acceptance Criteria

- A customer can cancel an order within **15 minutes** of order creation.
- After **15 minutes**, an order can no longer be canceled.
- If an order is already in **processing or shipped state**, it cannot be canceled.
- The system should emit an **OrderCanceled** event upon successful cancellation.

## Your Tasks

1. Requirement Analysis
   - What questions would you ask to clarify the story?
   - What assumptions would you verify before proceeding?

2. Solution Design
   - Provide a high-level **design approach**.
   - How would you modify the existing **Order Aggregate** to support this feature?
   - What **event-driven components** would be involved?

3. Technical Breakdown
   - Break down your solution into **small technical deliverables** (tickets/task4s).
   - Explain the priority and dependencies between these tasks.
   - Implement your solution that will meet the acceptance criteria.
     - **Note: Please add to github or source code repository of your choice.**