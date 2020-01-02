# SNS / SQS Demo

A  console app to show publishing to SNS and reading from SQS.

## Prerequisites

* Docker must be installed
* AWS CLI must be installed

## Setup

1. Install [LocalStack](https://github.com/localstack/localstack) using `pip`:

    ```cmd
    pip install localstack
    ```

2. Create a Docker container with LocalStack with command:

    ```cmd
    localstack start
    ```

3. Create the SNS topic:

    ```cmd
    aws --endpoint-url "http://localhost:4575" sns create-topic --name topic-name
    ```

4. Create the SQS queue:

    ```cmd
    aws --endpoint-url "http://localhost:4576" sqs create-queue --queue-name queue-name
    ```

5. Subscribe the queue to the topic:

    ```cmd
    aws --endpoint-url "http://localhost:4575" sns subscribe --protocol sqs --topic-arn arn:aws:sns:us-east-1:000000000000:topic-name --notification-endpoint arn:aws:sns:us-east-1:000000000000:queue-name
    ```

## Execution

Messages are published to a topic using an `SnsClient`; the total amount of messages can be configured with the variable `messagesToPublish`.

The `SqsClient` accepts a delegate when reading messages to perform a task prior to deleting said message - this is by no means set in stone.

The application will run indefinitely - press any key to exit the app.

## To Do

* Create a `docker-compose` to set up LocalStack
