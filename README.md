# 3DContentViewer

1. At the beginning of the application, you need to create a nickname and log in. Created nicks are saved in Firebase database.
2. The application interface is created according to the content of the json file downloaded from the specified url. 
3. It downloads models from the urls in the json file with the interaction of model buttons created according to the content of the json file. 
4. Downloaded models are shown in AR environment.
5. You can also message other users according to the nickname you create.
6. Users can send model urls among themselves and download and review these models.

A.	Scripts

a.	ContentSystem
1.	JsonDownloader:
It is used to download and deserialize the json file from the specified web address.

2.	ModelDownloader:
It is used to download the asset bundle from the web address that comes as a parameter.

3.	UIManager:
It listens to the JsonDownloader script and creates the interface according to the json content as soon as the json file is deserialized.

4.	ContentController:
When the model is to be downloaded, it runs before the Model Downloader script. Makes the arrangements related to the model content.

5.	ModelButtonController:
It is the script in the prefab of the buttons created in the UI according to the json file. It listens for the button click event and executes click actions. It instantiates models based on json content.

6.	LoadingController:
Shows the progress of the model download.


b.	LoginSystem
1.	LoginController:
It carries out the operations related to the user's login, logout and data.


c.	MessageSystem
1.	MessageSender:
It performs the process of sending the model url between users according to the inputs and buttons in the Message Box.

2.	MessageHandler:
Allows the user to listen to the database and open the popup when the message arrives.

3.	MessageController:
It shows the content of the incoming message to the user and provides the necessary references for downloading.


d.	ExceptionHandlers
1.	ExceptionHandler:
It is the super class of all Exception Handler types. It turns the object on and off after a certain period of time, like an instant warning subtitle.

2.	JsonDownloadExceptionHandler:
It follows the download process of the json file and informs the user when an error is encountered or when it is successful.

3.	ModelDownloadExceptionHandler:
It follows the model download process and informs the user when an error is encountered or when it is successful.

4.	LoginExceptionHandler:
Tracks errors on the user's login screen.

5.	MessageExceptionHandler:
It follows the message sending part and informs the user when an error is encountered or when there is a successful sending process.
