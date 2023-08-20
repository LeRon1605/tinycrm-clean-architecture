# Clean Architecture applying for TinyCRM project.
With **Clean Architecture**, the **Domain** and **Application layers** are at the centre of the design. This is known as the Core of the system.

The **Domain layer** contains enterprise logic and types and the **Application layer** contains business logic and types. The difference is that enterprise logic could be shared across many systems, whereas the business logic will typically only be used within this system.

**Core** should not be dependent on data access and other infrastructure concerns so those dependencies are inverted. This is achieved by adding interfaces or abstractions within **Core** that are implemented by layers outside of Core.

All dependencies flow inwards and **Core** has no dependency on any other layer. **Infrastructure** and **Presentation depend** on **Core**, but not on one another.

<p align="center">
  <img width="460" height="460" src="https://github.com/LeRon1605/tinycrm-clean-architecture/assets/78067510/ad3492ad-ed60-47c8-a5e1-ee2cc0358309">
</p>
