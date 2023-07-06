using Korzinka;
using System.Collections;
using System.Collections.Generic;

WorkWithFile file = new WorkWithFile();

Output ot = new Output();
Security sec = new Security();

if (sec.Role() == UserRole.Admin)
{
    ot.Menu(UserRole.Admin);


}


