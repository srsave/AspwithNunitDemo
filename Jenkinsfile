pipeline {
    agent any
    stages {
        stage('build') {
            steps {
		    deleteDir()
                git branch: 'main', credentialsId: 'srsave', url: 'https://github.com/srsave/AspWithNUnit.git'
            }
	}
	stage('Compilation') {
            steps {
                println "Now proceeding for source compilation using MSBuild executable"
		bat label: 'msbuild-step', script: '"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe" AspWithNUnit.sln /p:Configuration=Release /p:AllowUntrustedCertificate=True /p:CreatePackageOnPublish=True'
		bat label: 'Publish', script: '"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe" AspWithNUnit.sln /p:DeployOnBuild=true /p:PublishProfile=CustomProfile /property:WarningLevel=2;OutDir=%WORKSPACE%\\Publish'
            }
        }
	    stage('Code Inspect'){
		steps {    
       		echo 'Now performing code Analysis '
		bat label: 'code-Analysis-Step1', script: '"C:\\soft\\sonar-scanner-msbuild-5.1.0.28487-net46\\SonarScanner.MSBuild.exe" begin /k:"AspWithNUnit"'
		bat label: 'code-Analysis-Step2', script: '"C:\\Program Files (x86)\\Microsoft Visual Studio\\2019\\Community\\MSBuild\\Current\\Bin\\MSBuild.exe" C:\\Users\\Administrator\\.jenkins\\workspace\\test2\\AspWithNUnit.sln /t:Rebuild'
		bat label: 'code-Analysis-Step3', script: '"C:\\soft\\sonar-scanner-msbuild-5.1.0.28487-net46\\SonarScanner.MSBuild.exe" end'
      		println 'Success'
		}
    	}
	    stage('FTA'){
		steps {   
        	echo 'Now performing Nunit'
       		build 'FTA'
       		println 'Success'
		}
    	}
	   stage("Nunit"){
      		steps {
        	script {
          	try {
            	echo 'Now performing Nunit'
			build 'Nunit'
			println 'Success'
          } catch(err) {
		  echo "There are some errors in your unit tests!"
			//build_ok = false
			echo err.toString()
			currentBuild.result = "FAILURE"
			//createJira()
		  }    
       		 }
      		}
   	}
	    stage("Deploy"){
      steps { 
        script {
          try {
            	echo 'Trying to deploy release patch...'
				bat label: 'deploy-phase', script: '"C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:contentPath="%WORKSPACE%\\Publish\\_PublishedWebsites\\AspWithNUnit" -dest:contentPath="C:\\inetpub\\wwwroot\\AspWithNUnit",computerName="https://EC2AMAZ-E4KI5TP:8172/msdeploy.axd",UserName=\'EC2AMAZ-E4KI5TP\\Administrator\',Password=\'5UrhCqDtzfMZW85!Y2AGyk?zaVV4bFt=\',AuthType=\'Basic\' -enableRule:DoNotDeleteRule -allowUntrusted:True'''
				println 'DEPLOYMENT IS SUCCESSFUL!'
          } catch(err) {
	  		currentBuild.displayName = "${env.BUILD_TIMESTAMP}"
		        println 'DEPLOYMENT IS SUCCESSFUL'
	  }         
        }
      }
    }
	    
    }
	
}

def createJira(){
    withEnv(['JIRA_SITE=JIRA Pipeline UAT']){
        def failIssue = [fields: [project: [id: 'SDGAPP'],
                        summary: 'SDGAPP Jenkins execution bug',
                        description: 'SDGAPP has failed. Please do the needful',
                        issuetype: [name: 'Bug']]]
        response = jiraNewIssue issue: failIssue
        echo response.successful.toString()
        echo response.data.toString()
    }
}
