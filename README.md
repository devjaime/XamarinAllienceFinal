# XamarinAllienceFinal
XamarinAllinanceFinal autentificacion con facebook
INTRODUCCIÓN
Bienvenido al reto de codificación quinta #XamarinAlliance. En el tercer reto que ya utilizamos la nube para almacenar datos tabulares, estructurados. En este desafío que va a utilizar la nube para almacenar y recuperar datos no relacionales, por ejemplo, imágenes o documentos

El objetivo de este desafío codificación será para recuperar datos no relacionales de la nube .

Al completar este reto de codificación, se han aprendido cómo recuperar datos de Azure almacenamiento y uso en su aplicación Xamarin.

IMPORTANTE: Este es el desafío final para recibir su oficial Xamarin Alianza diploma ! Compruebe por debajo de lo que se necesita para recibir el crédito .

Descripción desafío
Empezando
Introducción a Azure Storage
Conexión a Azure de almacenamiento de Xamarin
recursos
Recibe crédito
finalización desafío
Obteniendo ayuda
DESCRIPCIÓN DESAFÍO
En este desafío que necesita para recuperar una imagen de una cuenta Azure de almacenamiento y utilizarlo dentro de la aplicación Xamarin. El archivo de imagen se almacena como una gota en el almacenamiento Blob y se protege mediante una firma acceso compartido.

Al almacenar los datos fuera de la aplicación móvil se puede modificar fácilmente o agregar datos sin tener que publicar una actualización de la aplicación a la tienda de aplicaciones.

Estos son los criterios para completar este reto:

Modificar la App Xamarin para recuperar un token de SAS del backend móvil.
Modificar la App Xamarin a recuperar una imagen de Azure Blob de almacenamiento utilizando el token SAS.
Obtener crédito por participar en la Alianza Xamarin.
EMPEZANDO
Código fuente

Puede continuar con la aplicación del tercer reto o cuarto desafío .

Si usted está construyendo su propia aplicación, puede continuar trabajando en ella y añadir la recuperación del archivo de imagen.

cuenta Azure Storage

Hemos establecido una cuenta de Azure de almacenamiento compartido para usted que contiene un archivo de imagen para que lo incluya en su aplicación. La cuenta de Azure de almacenamiento está disponible en https://xamarinalliance.blob.core.windows.net .

Como alternativa, puede crear su propia cuenta de Azure Storage. Siga estas instrucciones para crear una cuenta Azure Storage.

INTRODUCCIÓN A AZURE STORAGE
¿Cuál es Azure Storage?

Con Azure aplicaciones móviles puede almacenar datos estructurados y poner en práctica las operaciones CRUD contra almacenes de datos tabulares, pero es menos orientado hacia el almacenamiento y la gestión de grandes datos binarios, tales como documentos o archivos de imagen. Por otro lado, Microsoft Azure de almacenamiento es una solución de almacenamiento en la nube masivamente escalable, elástica, que proporciona las siguientes cuatro servicios: almacenamiento Blob, de almacenamiento de tabla, de almacenamiento de cola, y el almacenamiento de archivos. En este desafío que va a utilizar el almacenamiento Blob . Blob de almacenamiento almacena los datos de objeto no estructurados . Una mancha puede ser cualquier tipo de texto o binarios de datos, como un documento, archivo multimedia, o instalador de la aplicación. Blob de almacenamiento también se conoce como almacenamiento de objetos.

Con cuentas de almacenamiento Blob se distingue entre dos niveles de acceso :

Un caliente nivel de acceso que indica que se puede acceder con mayor frecuencia los objetos en la cuenta de almacenamiento. Esto le permite almacenar los datos de acceso a un costo más bajo.
Una fresca nivel de acceso que indica que se accederá con menos frecuencia los objetos en la cuenta de almacenamiento. Esto le permite almacenar los datos a un costo de almacenamiento de datos inferior.
Para que los datos serán utilizados por nuestra aplicación móvil que va a utilizar el nivel de acceso caliente porque tiene que ser accesible en cualquier momento.

Cada gota se organiza en un recipiente. Los contenedores también proporcionan una forma útil para asignar políticas de seguridad para grupos de objetos. A cuenta de almacenamiento puede contener cualquier número de contenedores, y un recipiente puede contener cualquier número de gotas, hasta el límite de capacidad 500 TB de la cuenta de almacenamiento.

Opciones de seguridad para Azure Storage
Por defecto sólo el propietario de la cuenta de almacenamiento puede acceder a los recursos de la cuenta de almacenamiento. Para la seguridad de sus datos, cada petición hecha contra los recursos deben ser autenticados en su cuenta. La autenticación se basa en un modelo de clave compartida . Manchas también puede ser configurado para soportar la autenticación anónima.

Si tiene que permitir a los usuarios el acceso controlado a los recursos de almacenamiento, a continuación, puede crear una firma de acceso compartido (SAS) . Un SAS es una señal de que se puede añadir a la URL que permite el acceso delegado a un recurso de almacenamiento. Un SAS contiene los permisos y un período de validez de tiempo para el acceso.

Por último, se puede especificar que un contenedor y sus manchas, o una mancha específica, están disponibles para el acceso público. Cuando indica que un contenedor o burbuja es público, cualquiera puede leer de forma anónima; no se requiere autenticación. contenedores y manchas públicos son útiles para exponer los recursos tales como medios de comunicación y documentos que están alojados en sitios web.

En este desafío que va a utilizar firmas de acceso compartido para proporcionar acceso a los datos de Azure Storage. Tenga en cuenta que en este caso específico que podría hacer que el público manchas porque no hay información privada contenida en ellos.

En el siguiente diagrama se puede ver la arquitectura para obtener una muestra de SAS y la recuperación de una gota de Azure Storage.

Diagrama simbólico de almacenamiento

CONEXIÓN A AZURE DE ALMACENAMIENTO DE XAMARIN
Como se mencionó antes, vamos a recuperar un archivo de imagen (logotipo Xamarin Alianza) de Azure Blob de almacenamiento. Para autenticar a la cuenta de almacenamiento, estaremos usando un identificador de SAS.

El archivo de imagen de la muestra está disponible en Azure Blob Storage aquí: https://xamarinalliance.blob.core.windows.net/images/XAMARIN-Alliance-logo.png

Para implementar este escenario, tendremos que realizar los siguientes pasos:

Añadir el paquete Azure Storage Client SDK NuGet a la aplicación Xamarin
Invocar el backend móvil para recuperar un token de SAS
Conectarse a la cuenta de almacenamiento utilizando el SDK del cliente Azure de almacenamiento, proporcionando el token SAS
Descargar el archivo de imagen
Añadir el almacenamiento Azure SDK de cliente
Para interactuar con Azure Storage hay un SDK del cliente, para lo cual hay un paquete NuGet: WindowsAzure.Storage .

IMPORTANTE: asegúrese de usar la versión 7.2.1 del paquete NuGet porque hay un problema con la versión V8 que da lugar a una 'excepción Método no implementado' en tiempo de ejecución.

Azure Storage Client SDK

Recuperar el token SAS
El token SAS se genera en el backend móvil. En el backend móvil compartida hemos añadido una API personalizada que permite la creación de un nuevo token SAS. El punto final para recuperar el token SAS para la cuenta de Azure de almacenamiento compartido (xamarinalliance.blob.core.windows.net) es:

http://xamarinalliancebackend.azurewebsites.net/api/StorageToken/CreateToken

Alternativamente, si usted está utilizando el servidor seguro, el punto final está disponible en:

http://xamarinalliancesecurebackend.azurewebsites.net/api/StorageToken/CreateToken

Para invocar esta API personalizado a partir de la aplicación Xamarin, puede aprovechar la InvokeApiAsync método en la MobileServiceClient ejemplo:

var  cliente = nueva  MobileServiceClient ( Constants.MobileServiceClientUrl );
var  contador = esperar  client.InvokeApiAsync ( "/ api / StorageToken / CreateToken");
El token SAS se devuelve como un valor de cadena.

Si usted ha construido su propio backend móvil, puede encontrar instrucciones sobre cómo crear una API personalizada en la documentación en línea .

Conectarse a la cuenta de almacenamiento utilizando el token SAS
Ahora vamos a utilizar el SDK del cliente Azure Storage para conectarse a Azure de almacenamiento de nuestra aplicación. En cuenta que necesitamos para proporcionar el SAS token y también especificar el nombre de la cuenta Azure Storage - para la cuenta de almacenamiento compartido esto es xamarinbackend .

cadena  storageAccountName = " xamarinalliance " ;

StorageCredentials  credenciales = nuevos  StorageCredentials ( contador );

CloudStorageAccount  cuenta = nuevo  CloudStorageAccount ( credenciales , storageAccountName, null, true);

var  cliente = account.CreateCloudBlobClient ();
Descargar el archivo de imagen
El último paso es descargar la burbuja de nuestra cuenta de almacenamiento. La URL completa de la mancha de imagen es https://xamarinalliance.blob.core.windows.net/images/XAMARIN-Alliance-logo.png . Como se puede deducir de esta URL, la imagen se encuentra en un recipiente llamado imágenes .

var  contenedor = client.GetContainerReference ( "imágenes");
var  blob = container.GetBlobReference ( "Xamarin-Alliance-logo.png");

MemoryStream  corriente = nuevo  MemoryStream ();

esperar  blob.DownloadToStreamAsync (corriente);
Ahora usted tiene la mancha real descargado en una corriente de objetos, que luego pueden ser aprovechados en la aplicación.

OBTENIENDO AYUDA
https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-dotnet-how-to-use-client-library#a-namecustomapiawork-with-a-custom- api
https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-shared-access-signature-part-1
https://developer.xamarin.com/guides/xamarin-forms/cloud-services/storage/azure-storage/
https://docs.microsoft.com/en-us/azure/app-service-mobile/app-service-mobile-xamarin-forms-blob-storage
https://azure.microsoft.com/en-us/services/app-service/mobile/
RECEPCIÓN DE CRÉDITO
Para recibir crédito por la Alianza Xamarin y obtener su diploma, tendrá que proporcionarnos un identificador único (GUID) a través de nuestro formulario online .

Para obtener este identificador único, tendrá que invocar una API personalizada en el backend móvil:

var  cliente = nueva  MobileServiceClient ( Constants.MobileServiceClientUrl );
var  GUID = esperar  client.InvokeApiAsync ( "/ api / XamarinAlliance / ReceiveCredit");
Una vez que haya recibido este GUID, puede presentarlo en línea en el formulario de envío . Esta será su boleto para obtener su diploma.

Los diplomas se distribuyen al final de cada mes para todos los desafíos completados antes de finales de junio de 2017.

DESAFÍO FINALIZACIÓN
Que haya desbloqueado este desafío cuando:

Modificar la App Xamarin para recuperar un token de SAS del backend móvil.
Modificar la App Xamarin a recuperar una imagen de Azure Blob de almacenamiento utilizando el token SAS.
Obtener crédito por participar en la Alianza Xamarin.
Cuando haya completado su reto de codificación, recoger su insignia y no dude en pío al respecto mediante el #XamarinAlliance hashtag.

OBTENIENDO AYUDA
Compruebe los foros Xamarin
Tweet hashtag #XamarinAlliance
Preguntas o cuestiones? Echa un vistazo a la FAQ o ingrese un problema
