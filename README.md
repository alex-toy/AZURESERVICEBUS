# AZURE SERVICE BUS

**Azure Service Bus** is a fully managed enterprise message broker with message queues and publish-subscribe topics. Service Bus is used to decouple applications and services from each other and provides reliable message queuing and durable publish/subscribe messaging. Some of the core messaging capabilities that are supported include Queues, Topics, Subscriptions, and Rules and Actions.


## Azure Service Queue

Let's use Queues in **Azure Service Bus** from a .NET application. We will create a **Queue**, send a message to it, the different message properties, consume messages from the it. We will consume it from an **Azure Function**. We will also set up Dependency Injection when working with Service Bus Clients and also use Managed Identity to connect securely with Service Bus Queues, which removes any need for sensitive configurations in the application.

### Service Bus

- create a **Service Bus**
<img src="/pictures/service_bus.png" title="service bus"  width="900">

- add a queue to it
<img src="/pictures/service_bus2.png" title="service bus"  width="900">

- grab the default connection string
<img src="/pictures/service_bus3.png" title="service bus"  width="900">

### API

- create a .NET Core API version 6

- add packages
```
Azure.Messaging.ServiceBus
Microsoft.Extensions.Azure
```

- run the app and try out the Post method
<img src="/pictures/service_bus4.png" title="service bus"  width="900">

- see the message in service bus
<img src="/pictures/service_bus5.png" title="service bus"  width="900">

- in service bus explore, you can peek messages
<img src="/pictures/service_bus6.png" title="service bus"  width="900">

### Azure Function

- create an *Azure Function*. Choose *Service Bus Queue Trigger*
<img src="/pictures/azure_function.png" title="service bus"  width="900">

- run both the app and the azure function and see that the azure function has been triggered
<img src="/pictures/azure_function2.png" title="service bus"  width="900">

- messages that throw an exception end up in the dead letter queue
<img src="/pictures/azure_function3.png" title="service bus"  width="900">

- you can modify the number of times the messages that throw an exception will be sent back into the queue
<img src="/pictures/azure_function4.png" title="service bus"  width="900">

### Managed identity

- in the *Access Control (IAM)* section, add a role
<img src="/pictures/role.png" title="managed identity"  width="900">

- add a member
<img src="/pictures/role2.png" title="managed identity"  width="900">
<img src="/pictures/role3.png" title="managed identity"  width="900">

- in visual studio Tools/Options, select *Azure Service Authentication* and make sure you have the right user selected
<img src="/pictures/role4.png" title="managed identity"  width="900">

- in the Azure function project add package. You need to update to version 5.3 in case
```
Microsoft.Azure.WebJobs.Extensions.ServiceBus
```

- we can now remove the connection string from the controller


## Topics and Subscriptions

We will use **Topics** and **Subscriptions** in Azure Service Bus from a .NET application. We will create a new Topic, send messages to it, add multiple subscribers and process the messages. We will also add Filters and Actions when creating subscriptions and see how that affects message processing.

### Service Bus

- create a **Service Bus**. Since we will be using topics, you need to choose the *Standard Pricing Tier*
<img src="/pictures/service_bus_topic.png" title="service bus"  width="900">

- create a topic
<img src="/pictures/service_bus_topic2.png" title="service bus"  width="900">

- create a subscription
<img src="/pictures/service_bus_topic3.png" title="service bus"  width="900">

- run the app and peek one message
<img src="/pictures/service_bus_topic4.png" title="service bus"  width="900">

### Azure Function

- create an *Azure Function*. Choose *Service Bus Topic Trigger*
<img src="/pictures/af_topic.png" title="azure function topic"  width="900">

- in the Azure function project add package. You need to update to version 5.3 in case
```
Microsoft.Azure.WebJobs.Extensions.ServiceBus
```

- run the app
<img src="/pictures/af_topic2.png" title="azure function topic"  width="900">