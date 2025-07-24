# Step 1: Requirement Analysis

Let's not start coding before we fully understand the ask. Here are the questions I'd want answered:

## Questions to Clarify

1. **Timekeeping**  
   Are we comparing the current system time against the order's `CreatedAt` timestamp?  
   - What time zone is used?  
   - Should this be based on the exact UTC timestamp of order creation?  
   - If not, we'll have inconsistencies across time zones.

2. **State Check**  
   What qualifies as “processing”?  
   - Is there a defined list of statuses (e.g., `NEW`, `PROCESSING`, `SHIPPED`, `DELIVERED`)?

3. **Concurrency**  
   What happens if cancellation and processing are triggered at the same time?

4. **Error Handling**  
   How do we communicate back to the user when cancellation is rejected?

5. **Idempotency**  
   Can users attempt to cancel multiple times?  
   - Should we guard against double emits of `OrderCanceled`?

6. **Event Consumer**  
   Who listens to `OrderCanceled`?  
   - Is there a need for rollback/compensation in other systems?

---

## Assumptions (to be confirmed)

- Order state is stored in the aggregate, likely as an enum or value object.
- There's an event dispatcher that handles emitting domain events like `OrderCanceled`.
- Orders have a `CreatedAt` property stored in **UTC**.
