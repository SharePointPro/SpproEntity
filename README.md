# SharePointPro Entity Framework
SharePoint Entity Framework Created By Michael Tippett from SharePointPro.

A SharePoint First Entity Framework to simplify access to SharePoint Online or On Premisis.

Automatically generate SharePoint lists as Entitys. A context is created with all the entity repository for quick access.

CRUD Actions has never been simpler.

# Installation
* Create your SharePoint Lists.
* Download library and reference SpproFramework in your project.
* Run SpproFramework.Migrate application and connect to your SharePoint Site. Select Lists and fields and create .cs entity files and Context
* Instantiate SpContext and access your SharePoint list via a strongly typed created library.
* Check out wiki https://github.com/SharePointPro/SpproEntity/wiki/SpproFrameWork-Migrate for pictures.

# Example Code
```
        SecureString password = FetchPasswordFromConsole(); //Create Secure String Password
        try
          {
            using (var context = new ClientContext(webSPOUrl))  //Create Client Context using Shaerpoint URL
            {
                context.Credentials = new SharePointOnlineCredentials(userName, password);  //Use Credentials
                SpContext SpoContext = new SpContext(context); //Sp Context. Class Named via the SpproFramework GUI
                var property = SpContext.Properties.Query("Id=3")[0];  //Get First entity with ID = 3, Any field can be used to query
                property.Description = "My House"; //Update the Entity Property
                SpContext.UpdateOrCreate(property); //Save Change back to SharePoint
            }   
        }
```           

# Requirements
Your project must have the following References:
* Microsoft.SharePoint.Client
* Microsoft.SharePoint.Client.Runtime
* System.Device.Location

# Lazy Loading
Sppro Entities include lazy loading, simply add a navigation property in the created model, decorate and the Sppro Entity Engine will automatically load objects during any GET call.

Model Snippit Example:
```
    [SpproNavigationAttribute(NavigationProperty = true, LookupField="QuoteID")]
    public virtual ICollection<QuoteRooms> QuoteRooms { get; set; }
```

# More to come
Sppro Entity is still very much a work in progress. If you have any suggestion please contact me via my webpage.
See more here: http://www.sharepointpro.com.au

I plan on introducing LINQ to framework. 
           
