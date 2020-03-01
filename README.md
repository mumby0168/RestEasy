# RestEasy
A simple, quick and effective way to make rest api's in asp.net core

This is a new project please see the roadmap seciton to see what is planned to be added :)

## Inspiration
The main inspiration for this was the hasssle for creating controllers and defining routes. In addition controllers can very often violate the first principle of SOLID, Single Reponsbility this in the case of a API can make testing difficult and a pain and often sends people in the wrong direction. REST is so common now and people want a quick way to make an API this is where RestEasy comes in it allows with a few classes and extensions to a empty ASP.NET CORE application to have a working REST API.

## Patterns and Practices Considered
RestEasy aims to bring best practice and simplicity into a API. It makes use of the repository pattern to decouple the framework from any single persitance choice. It also provides a simple API to map a domain object to a data transfer object (DTO) this is aiming to help with a Domain Driven Design implementation. It also follows the simple idea of extensions to the ASP.NET CORE framework.

## Getting Started
This section will show the basic example that currently gets a API up and running.
