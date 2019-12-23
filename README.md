# SbDuplicateDetection

This sample demonstrate how duplicate detection works in azure service bus queues.

Pre-Requisites
____________
Azure Subscription
Azure Service Bus namespace created in Standard Pricing tier or above. Duplicate detection doesn't work with basic pricing tier.
Azure Service Bus Queue with Duplicate Detection enabled

***Please replace queueName and ServiceBusConnectionstring with your settings in program.cs

Service bus with Duplicate detection enabled receiving messages within specification duplication time window will reject messages in case same MessageId property is received for multiple messages. Only messages will unique MessageId will be retained in the queue.
