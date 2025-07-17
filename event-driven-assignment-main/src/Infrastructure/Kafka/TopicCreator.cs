using Confluent.Kafka;
using Confluent.Kafka.Admin;

namespace Infrastructure.Kafka;

public sealed class TopicCreator(string bootstrapServers)
{
    private readonly string _bootstrapServers = bootstrapServers;

    public async Task EnsureTopicExistsAsync(string topicName, int numPartitions = 3, short replicationFactor = 1)
    {
        using var adminClient = new AdminClientBuilder(new AdminClientConfig { BootstrapServers = _bootstrapServers }).Build();
        try
        {
            var topics = adminClient.GetMetadata(TimeSpan.FromSeconds(5));
            if (topics.Topics.Exists(t => t.Topic == topicName))
            {
                Console.WriteLine($"Topic '{topicName}' already exists.");
                return;
            }

            await adminClient.CreateTopicsAsync(
            [
                new() { Name = topicName, NumPartitions = numPartitions, ReplicationFactor = replicationFactor }
            ]);

            Console.WriteLine($"Created topic: {topicName}");
        }
        catch (CreateTopicsException e) when (e.Results[0].Error.Code == ErrorCode.TopicAlreadyExists)
        {
            Console.WriteLine($"Topic '{topicName}' already exists.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error creating topic '{topicName}': {ex.Message}");
        }
    }
}