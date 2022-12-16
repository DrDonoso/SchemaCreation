# Schema creation

An EventHub Schema Registry command line tool

## Give a Star! :star:

If you like or are using this utility, please give it a star. Thanks!

## Sample usage

```powershell
> schemacreation Apply -t <AADTenantId> -c <ClientId> -s <ClientSecretKey> -e <EventHub> -g <SchemaGroup> -f <File>
```

With this tool you can register a schema to the schema group.

⚠️ Schema Group should exists.

## How to install

At the moment, we have packaged the solution as a dotnet global tool. You only need dotnet core (2, 3 or NET5) installed in your machine and install as a global tool typing:

```powershell
>   dotnet tool install --global schemacreation
```
