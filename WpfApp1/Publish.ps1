az login -u $Email -p $Password
Set-ExecutionPolicy RemoteSigned 
Import-Module Azure
Get-AzurePublishSettingsFile
Import-AzurePublishSettingsFile -PublishSettingsFile $PublishingProfile
Get-AzureSubscription 
Select-AzureSubscription -SubscriptionName 'Visual Studio Enterprise' 
Set-AzureSubscription -SubscriptionId "c419c5ef-a0bb-4e92-a1fc-eee898bb86b3" 
cd $ProjectDirectory
C:\Windows\Microsoft.NET\Framework\v4.0.30319\msbuild.exe
Import-AzurePublishSettingsFile -PublishSettingsFile $PublishingProfile
Publish-AzureWebsiteProject -Name $WebAppName -Package $ProjectDirectory -Slot $Slot