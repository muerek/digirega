# DigiRega

DigiRega helps to collect and manage entries for rowing competitions on race day, paperless and contactless. Club managers can easily enter late entries, crew changes and withdrawals online. Organizers can manage entries within DigiRega.

## Prerequisites

- ASP.NET 5 runtime (if not using the Docker image)
- PostgreSQL database
- Access to an SMTP server for sending emails

## Getting started

### Configuration

Make sure to expose the following app settings to DigiRega. Defaults can be overridden by setting their keys to another value.

These settings are used to establish database connectivity:

Key | Value | Default
---|---|---
Database:Server | URI or IP of the database host | digirega.db
Database:Port | Database port on the host | 5432
Database:DbName | Name for the DigiRega database | digirega
Database:User | Username for database access | (none)
Database:Password | Password for database access | (none)

DigiRega sends out credentials and notifications via email and needs to access an SMTP server to do so:.

Key | Value | Default
---|---|---
Emailing:SmtpServer | SMTP server URI | (none)
Emailing:SmtpPort | Port for SMTP server connection | 465
Emailing:UseSsl | Boolean flag if SMTP connection should use SSL | true
Emailing:User | Username for SMTP server authentication | (none)
Emailing:Password | Password for SMTP server authentication | (none)
Emailing:FromAddress | Email address shown as sender for emails | (none)
Emailing:FromName | Friendly name for email sender | DigiRega

Additional information is stored in further app settings:

Key | Value | Default
---|---|---
LoginUrl | URI of the login page for use in invitation emails | (none)
JwtSigningKey | Key used for symmetric JWT signing | (none)

### Startup

The entry point for running DigiRega is the `DigiRega.Server` project.

If you are using the Docker image, you can use the included `docker-compose.yml` to spin up a database container at the same time as DigiRega.

### First steps

- Navigate to the DigiRega homepage.
- Login using username `admin` with default password `admin`. Change this as soon as possible.

## Limitations

- Crew members cannot be of different clubs. You will need to add a new, separate club to represent the combined crew today.
- The server does not paginate results (and therefore neither does the client). This should not be a problem for small events, but may become an issue for large events.
- Users may encounter error messages and validation messages in English and/or somewhat unfriendly explanations in some cases.
- I am not really convinced of my client-side error handling, but it's working and should be good enough for now.

You can also check the issues for a more detailed rundown.

## Licensing

DigiRega is licensed under the included AGPL license. If this does not fit your needs, dual-licensing may be possible, in particular if you are hosting rowing competitions for U15 in North Rhine-Westphalia. Please reach out to me.