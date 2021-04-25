# Authorization

Access to certain functionality is governed by user roles. These user roles restrict access to the server-side RESTful API as well as UI elements and pages in the client.

This document outlines the basic rights each role is supposed to have. Please see the source code for details on the implementation.

There are three roles:

## Manager

Someone responsible for a single club

- Can create entries
- Can see their own entries

## Staff

Someone from the organizing comittee

- Can see all entries
- Can create entries on behave of a manager
- Can move and entry through its lifecyle (approve, reject, mark as done)
- Can add, edit and delete managers

## Admin

Someone from the organizing comittee responsible for the app

- Can do everything a Staff user can do
- Can add, edit and delete staff users
- Can modify app settings