# Electronic Component Color Band Decoder.

Allows user to select the band colors on their electrical component (currently resistor).  As soon as four valid bands have been selected the Ohms is calculated using the standard color code table per IEC 60062:2016 https://en.wikipedia.org/wiki/Letter_and_digit_code#IEC_60062 and is displayed back to the user.

## Getting Started

Built using Telerik's KendoUI opensource version "kendo-ui-core" found at https://github.com/telerik/kendo-ui-core.


### Installing

How to build Kendo UI Core from (https://github.com/telerik/kendo-ui-core):
        Clone a copy of the repository by running

        git clone https://github.com/telerik/kendo-ui-core.git
        Enter the repository directory

        cd kendo-ui-core
        Initialize the submodule repository

        git submodule update --init
        Run the build script:

        npm run build

take the contents of the 'js' folder from the above and paste into this project's ..\Scripts\kendo
take th contents of the 'styles' folder from the aboveand paste into this project's ..Styles\kendo


## Built With

Visual Studio 2017
.NET Framework 4.61
Microsoft.AspNet.Mvc version="5.2.3"
bootstrap version="3.3.2"
jQuery version="1.10.2"
NUnit 3.10.1


