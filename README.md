# RabbitMQ

O que é RabbitMQ:
RabbitMQ é um servidor de mensageria de código aberto (open source) desenvolvido em Erlang, implementado para suportar mensagens em um protocolo denominado Advanced Message Queuing Protocol (AMQP). Ele possibilita lidar com o tráfego de mensagens de forma rápida e confiável, além de ser compatível com diversas linguagens de programação, possuir interface de administração nativa e ser multiplataforma.
Dentre as aplicabilidades do RabbitMQ estão possibilitar a garantia de assincronicidade entre aplicações, diminuir o acoplamento entre aplicações, distribuir alertas, controlar fila de trabalhos em background.
Este artigo tem como objetivo demonstrar um simples exemplo de como utilizar o RabbitMQ.
Conceitos
Mensagem:
Uma mensagem é dividida em duas partes:
•	Payload – é o corpo com os dados que serão transmitidos. Suporta vários tipos de dados como um array json até um filme mpeg.
•	Label – é responsável pela descrição do payload e também como o RabbitMQ saberá quem irá receber a mensagem.
Fila:
 
Onde as mensagens ficam e são retiradas pelos consumers.
Publisher:
 
É o responsável por incluir cada nova mensagem na fila, ou seja enviar a mensagem.
Consumer:
 
Como diz o próprio nome é o agente responsável por consumir, retirar, a informação da fila.

