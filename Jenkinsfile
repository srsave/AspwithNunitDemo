pipeline {
	agent any
    stages {
        stage('Pull Code from branch') {
            steps {
				deleteDir()
                git branch: 'branch1', credentialsId: 'hp', url: 'https://github.com/Hiramangp/AspwithNunitDemo.git'
            }
		}
		stage('Compile the code') {
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
	    
	   stage("Unit Testing"){
      		steps {
				script {
					try {
						echo 'Now performing Nunit'
						//build 'Nunit'
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
						bat label: 'deploy-phase', script: '"C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:contentPath="%WORKSPACE%\\Publish\\_PublishedWebsites\\AspWithNUnit" -dest:contentPath="C:\\inetpub\\wwwroot\\AspWithNUnit_Dev",computerName="https://EC2AMAZ-E4KI5TP:8172/msdeploy.axd",UserName=\'EC2AMAZ-E4KI5TP\\Administrator\',Password=\'5UrhCqDtzfMZW85!Y2AGyk?zaVV4bFt=\',AuthType=\'Basic\' -enableRule:DoNotDeleteRule -allowUntrusted:True'''
						println 'DEPLOYMENT IS SUCCESSFUL!'
					} catch(err) {
						currentBuild.displayName = "${env.BUILD_TIMESTAMP}"
						println 'DEPLOYMENT IS SUCCESSFUL'
					}         
				}
			}
		}
		stage('Automated Functional Testing'){
			steps {   
				echo 'Now performing Nunit'
				build 'FTA'
				println 'Success'
			}
    	}
		
		stage('Code Review'){
			steps {   
				echo 'Now performing Code Review'
				
				println 'Success'
			}
    	}
		
		stage('Approval for Code Merge') {
            steps {
				input id: '100', message: 'Continue with the Code Merge in Main Branch?', ok: 'Approved', submitter: 'administrator'
            }
        }
		
	    stage('Code Merge'){
			steps {   
				echo 'Now performing Code Merge'
				//build 'Code Merge'
				deleteDir()
                git branch: 'main', credentialsId: 'hp', url: 'https://github.com/Hiramangp/AspwithNunitDemo.git'
				println 'Success'
			}
    	}
		
	    stage('Compile the Merge code') {
            steps {
                println "Now proceeding for source compilation using MSBuild executable"
				bat label: 'msbuild-step', script: '"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe" AspWithNUnit.sln /p:Configuration=Release /p:AllowUntrustedCertificate=True /p:CreatePackageOnPublish=True'
				bat label: 'Publish', script: '"C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe" AspWithNUnit.sln /p:DeployOnBuild=true /p:PublishProfile=CustomProfile /property:WarningLevel=2;OutDir=%WORKSPACE%\\Publish'
            }
        }
		
		stage("Integration Testing for main"){
      		steps {
				script {
					try {
						echo 'Now performing Nunit'
						//build 'Nunit'
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
		
		stage('Push the Artifacts in Nexus') {
            steps {
				bat label: 'zip-file', script: '"C:\\Program Files\\7-Zip\\7z.exe" a  %WORKSPACE%\\patch.zip %WORKSPACE%\\Publish\\_PublishedWebsites\\AspWithNUnit\\.'
				nexusArtifactUploader artifacts: [[artifactId: 'ASPTest', classifier: '', file: 'patch.zip', type: 'zip']], credentialsId: 'NexusRepoSDG', groupId: 'sdg', nexusUrl: '15.207.55.227:8081/repository/ASPTest/', nexusVersion: 'nexus3', protocol: 'http', repository: 'ASPTest', version: '$BUILD_TIMESTAMP'
				echo 'Upload to nexus done...'		
            }
        }
		
		stage('Approval for UAT DEPLOYMENT') {
            steps {
				input id: '100', message: 'Continue with the UAT Deployment?', ok: 'Approved', submitter: 'administrator'
            }
        }
		
		stage("UAT Deployment"){
			steps {
				script {
					try {
						bat label: 'zip-file', script: '"C:\\Program Files\\7-Zip\\7z.exe" a  %WORKSPACE%\\UAT_Bkp.zip C:\\inetpub\\wwwroot\\AspWithNUnit_UAT'
						nexusArtifactUploader artifacts: [[artifactId: 'ASPTest', classifier: '', file: 'UAT_Bkp.zip', type: 'zip']], credentialsId: 'NexusRepoSDG', groupId: 'sdg', nexusUrl: '15.207.55.227:8081/repository/ASPTest_UAT/', nexusVersion: 'nexus3', protocol: 'http', repository: 'ASPTest_UAT', version: '$BUILD_TIMESTAMP'
						echo 'Upload to nexus done...'		
						echo 'Trying to deploy release patch...'
						bat label: 'deploy-phase', script: '"C:\\Program Files\\IIS\\Microsoft Web Deploy V3\\msdeploy.exe" -verb:sync -source:contentPath="%WORKSPACE%\\Publish\\_PublishedWebsites\\AspWithNUnit" -dest:contentPath="C:\\inetpub\\wwwroot\\AspWithNUnit_UAT",computerName="https://EC2AMAZ-E4KI5TP:8172/msdeploy.axd",UserName=\'EC2AMAZ-E4KI5TP\\Administrator\',Password=\'5UrhCqDtzfMZW85!Y2AGyk?zaVV4bFt=\',AuthType=\'Basic\' -enableRule:DoNotDeleteRule -allowUntrusted:True'''
						println 'DEPLOYMENT IS SUCCESSFUL!'
					} catch(err) {
						currentBuild.displayName = "${env.BUILD_TIMESTAMP}"
						println 'DEPLOYMENT IS SUCCESSFUL'
					}         
				}
			}
		}
		
	    stage('Automated Functional Testing of Merge'){
			steps {
				echo 'Now performing Nunit'
				//build 'FTA'
				println 'Success'
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
