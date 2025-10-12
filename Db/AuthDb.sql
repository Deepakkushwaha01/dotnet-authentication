CREATE TABLE "AspNetUsers" (
  "Id" nvarchar(450) PRIMARY KEY,
  "UserName" nvarchar(256),
  "NormalizedUserName" nvarchar(256),
  "Email" nvarchar(256),
  "NormalizedEmail" nvarchar(256),
  "EmailConfirmed" bit,
  "PasswordHash" nvarchar(max),
  "SecurityStamp" nvarchar(max),
  "ConcurrencyStamp" nvarchar(max),
  "PhoneNumber" nvarchar(max),
  "PhoneNumberConfirmed" bit,
  "TwoFactorEnabled" bit,
  "LockoutEnd" datetimeoffset,
  "LockoutEnabled" bit,
  "AccessFailedCount" int
);

CREATE TABLE "AspNetRoles" (
  "Id" nvarchar(450) PRIMARY KEY,
  "Name" nvarchar(256),
  "NormalizedName" nvarchar(256),
  "ConcurrencyStamp" nvarchar(max)
);

CREATE TABLE "AspNetUserRoles" (
  "UserId" nvarchar(450),
  "RoleId" nvarchar(450)
);

CREATE TABLE "AspNetUserClaims" (
  "Id" int PRIMARY KEY,
  "UserId" nvarchar(450),
  "ClaimType" nvarchar(max),
  "ClaimValue" nvarchar(max)
);

CREATE TABLE "AspNetRoleClaims" (
  "Id" int PRIMARY KEY,
  "RoleId" nvarchar(450),
  "ClaimType" nvarchar(max),
  "ClaimValue" nvarchar(max)
);

CREATE TABLE "AspNetUserLogins" (
  "LoginProvider" nvarchar(450),
  "ProviderKey" nvarchar(450),
  "ProviderDisplayName" nvarchar(max),
  "UserId" nvarchar(450),
  PRIMARY KEY ("LoginProvider", "ProviderKey")
);

CREATE TABLE "AspNetUserTokens" (
  "UserId" nvarchar(450),
  "LoginProvider" nvarchar(450),
  "Name" nvarchar(450),
  "Value" nvarchar(max),
  PRIMARY KEY ("UserId", "LoginProvider", "Name")
);

ALTER TABLE "AspNetUserRoles" ADD FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id");

ALTER TABLE "AspNetUserRoles" ADD FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id");

ALTER TABLE "AspNetUserClaims" ADD FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id");

ALTER TABLE "AspNetRoleClaims" ADD FOREIGN KEY ("RoleId") REFERENCES "AspNetRoles" ("Id");

ALTER TABLE "AspNetUserLogins" ADD FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id");

ALTER TABLE "AspNetUserTokens" ADD FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id");
