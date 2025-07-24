## Task Breakdown

| # | Task | Description | Priority | Depends On |
|---|------|-------------|----------|------------|
| 1 | Domain: `Cancel()` method | Implement cancellation logic inside `Order` aggregate. Enforce time window using `IClock`. Reject invalid states. | High | — |
| 2 | Domain Event: `OrderCanceled` | Raise internal domain event from the aggregate when cancellation succeeds. | High | 1 |
| 3 | Application Command: `CancelOrderCommand` | Create command and DTO to encapsulate input (OrderId, CorrelationId). | High | — |
| 4 | Command Handler | Implement `CancelOrderCommandHandler`: load, cancel, save, publish. Inject `IClock` and `ITopicProducer`. | High | 1, 2, 3 |
| 5 | Event Contract: `Shared.Events.V1.OrderCanceled` | Define Kafka message interface in shared namespace per guidelines. | High | — |
| 6 | API Endpoint | Add `POST /orders/{id}/cancel` to invoke command handler. Extract `CorrelationId` from header. | Medium | 4 |
| 7 | Publish to Kafka | Use MassTransit's `_topicProducer` to publish `OrderCanceled` event from handler. | High | 4, 5 |
| 8 | Kafka Consumer Registration | Register `OrderCanceledConsumer` using MassTransit’s `ReceiveEndpoint`. | Medium | 5 |
| 9 | Consumer Logic | Implement `OrderCanceledConsumer` to handle downstream side effects. | Medium | 8 |
| 10 | Unit Tests: Domain Logic | Mock `IClock`, test 15-minute boundary edge cases (14:59, 15:00, etc.), invalid states, valid flows. | High | 1 |
| 11 | Integration Tests | End-to-end test for API → command → domain → publish flow. | Medium | 7 |

---

### Sprint/Dev Notes

- All time checks must use `IClock.UtcNow`
- Controllers must stay thin; domain logic lives in aggregates
- Unit tests must hit edge cases around 15-minute cutoff
