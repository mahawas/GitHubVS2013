
import org.activiti.engine.delegate.DelegateExecution;
import org.activiti.engine.delegate.JavaDelegate;
import org.activiti.engine.impl.el.FixedValue;
import org.activiti.engine.impl.el.Expression;

import java.io.*;
import java.net.*;


public class Operations implements JavaDelegate 
{
	private Expression  URL;
	private Expression  ID;
	private Expression  ReturnValue;
	private Expression  RequestMethod;
	  
	public void execute(DelegateExecution execution) throws Exception 
	{
		String strURL = (String)URL.getValue(execution);
		//String strID = String.valueOf(ID.getValue(execution));		
		String strReturnValue = (String)ReturnValue.getValue(execution);
		String strRequestMethod = (String)RequestMethod.getValue(execution);
								
	 	URL url = new URL(strURL);
	    	
		HttpURLConnection connection = (HttpURLConnection)url.openConnection();
		connection.setDoInput(true);
		connection.setRequestMethod(strRequestMethod);
		connection.setUseCaches(false); 
		connection.setAllowUserInteraction(false); 
		connection.setReadTimeout(30 * 1000);
		connection.setRequestProperty("Connection", "Keep-Alive");
		
	        /*connection.setRequestProperty("Authorization", 
	            "Basic " +  Base64.encode("username" + ":" + "password"));
	            */

	        
	        
        BufferedReader in = new BufferedReader(new InputStreamReader(connection.getInputStream()));

        String line = "";
        StringBuilder sb = new StringBuilder();
        while ((line = in.readLine()) != null) 
        {
            sb.append(line);
        }
        in.close();
        connection.disconnect();
        
         
		 if (strReturnValue != null) 
		 {			 
			 execution.setVariable(strReturnValue, sb.toString());
		 }
		    
	}
}

/*
 import org.activiti.engine.delegate.DelegateExecution;
 
import org.activiti.engine.delegate.JavaDelegate;

import org.activiti.engine.impl.el.FixedValue;
import org.activiti.engine.impl.el.Expression;

import org.apache.cxf.endpoint.Client;
import org.apache.cxf.jaxws.endpoint.dynamic.JaxWsDynamicClientFactory;


import java.util.ArrayList;
import java.util.StringTokenizer;

public class Operations implements JavaDelegate 
{
	  private Expression  wsdl;
	  private Expression  operation; 
	  //private String parameters;  
	  private Expression  returnValue;
	  
	public void execute(DelegateExecution execution) throws Exception 
	{
		String var = (String) execution.getVariable("Subject");
		var = "Changed";
		execution.setVariable("Subject", var);
		
		/*
		 String wsdlString = (String)wsdl.getValue(execution);

		    JaxWsDynamicClientFactory dcf = JaxWsDynamicClientFactory.newInstance();
		    Client client = dcf.createClient(wsdlString);

		    ArrayList paramStrings = new ArrayList();
		    if (parameters!=null) {     
		      StringTokenizer st = new StringTokenizer( (String)parameters.getValue(execution), ",");
		      while (st.hasMoreTokens()) {
		        paramStrings.add(st.nextToken().trim());
		      }     
		    }   
		    Object response = client.invoke((String)operation.getValue(execution), paramStrings.toArray(new Object[0]));    
		    if (returnValue!=null) {
		      String returnVariableName = (String) returnValue.getValue(execution);
		      execution.setVariable(returnVariableName, response);
		    }
		    */
		/*
		 String wsdlString = (String)"http://localhost:51544/test.asmx?wsdl";//wsdl;
		 String operationString = (String) "HelloWorld"; //operation
		 String returnValueString = (String) "Subject"; //returnValueString
		 
		 JaxWsDynamicClientFactory dcf = JaxWsDynamicClientFactory.newInstance();
         /*Client client = dcf.createClient(wsdlString);

		 
		 Object response = client.invoke(operationString, null);    
		 if (returnValue!=null) 
		 {
			 String returnVariableName = (String) returnValueString;
			 execution.setVariable(returnVariableName, response);
		 }*//*
		    
	}
}*/