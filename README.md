mvczin
======

Extensões para o ASPNET MVC

Custom Validators
    
    - CNPJ Validation Attribute com suporte a client side unobtrusive validation
    - CPF Validation Attribute com suporte a client side unobtrusive validation
    - [Em breve] Validation Attribute para cartões de crédito http://www.linkedin.com/groups/Hi-Everyone-Currently-I-am-1779887.S.5907869343005294595?view=&item=5907869343005294595&type=member&gid=1779887&trk=eml-b2_anet_digest-hero-10-hero-disc-disc-0&midToken=AQFsuoWBJaaaMA&fromEmail=fromEmail&ut=0MQOJK9cxDgCo1
    - [Em breve] Validation Attribute para Inscrição Estadual
    
Usando os Atributos
    
    - Model
        [Display(Name = "CNPJ")]
        [Cnpj(ErrorMessage = "O valor '{0}' é inválido para CNPJ")]
        public string Cnpj { get; set; }

        [Display(Name = "CPF")]
        [Cpf(ErrorMessage = "O valor '{0}' é inválido para CPF")]
        public string Cpf { get; set; }
        
    - Scripts
        jquery-1.10.2.js
        jquery.validate.js
        jquery.validate.unobtrusive.js
        jquery.validate.unobtrusive.method.cnpj.js
        jquery.validate.unobtrusive.method.cpf.js

Version
----

0.1

Tech
-----------

As extensões dependem de uma serie de bibioteclas de terceiros

* [jquery] - o framework js mais legal de todos os tempos
* [jquery.validate] - jquery plugin de validação 
* [jquery.validate.unobtrosive] - jquery plugin de validação não obstrusivo 

Contributors
-----------

* lucas silva ( [github], [bitbucket] )

License
----

MIT

**Free Software, Hell Yeah!**
[github]:https://github.com/lucas-silva
[bitbucket]:https://bitbucket.org/lucassilva
[jquery]:https://www.nuget.org/packages/jQuery/
[jquery.validate]:https://www.nuget.org/packages/jQuery.Validation/
[jquery.validate.unobtrosive]:https://www.nuget.org/packages/Microsoft.jQuery.Unobtrusive.Validation/
