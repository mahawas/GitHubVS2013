1- http://www.attuneuniversity.com/blog/change-database-from-h2-to-mysql-in-activiti.html
2- https://www.youtube.com/watch?v=ffKcGOoWjb8

upload the process defination through the rest not throug the explorer. to avoid loss the enum.

http://wrongtracks.blogspot.ae/2013/07/comparison-of-different-bpmn-modelling.html
http://forums.activiti.org/content/deployment-failed-when-calling-wsdl-activiti-explorer
http://www.jorambarrez.be/blog/2013/03/25/bug-on-jdk-1-7-0_17-when-using-scripttask-in-activiti/
http://www.jorambarrez.be/blog/category/activiti/


to delete all the process run the below script

DELETE FROM activiti.act_ru_identitylink;
DELETE FROM activiti.act_ru_task;
DELETE FROM activiti.act_ru_variable;
DELETE FROM activiti.act_ru_execution where PARENT_ID_ is not null;
DELETE FROM activiti.act_ru_execution ;
delete FROM activiti.act_re_procdef;


TRUNCATE `activiti`.`act_hi_actinst`;
TRUNCATE `activiti`.`act_hi_attachment`;
TRUNCATE `activiti`.`act_hi_comment`;
TRUNCATE `activiti`.`act_hi_detail`;
TRUNCATE `activiti`.`act_hi_identitylink`;
TRUNCATE `activiti`.`act_hi_procinst`;
TRUNCATE `activiti`.`act_hi_taskinst`;
TRUNCATE `activiti`.`act_hi_varinst`;

delete FROM activiti.act_hi_actinst;



http://activiti.org/javadocs/org/activiti/engine/delegate/VariableScope.html#getVariable(java.lang.String, boolean)


https://github.com/Activiti/Activiti/blob/master/modules/activiti-engine/src/test/resources/org/activiti/examples/bpmn/tasklistener/ScriptTaskListenerTest.bpmn20.xml

http://forums.activiti.org/content/possible-store-data-step



http://www.jorambarrez.be/blog/2013/07/23/advanced-activiti-scripting/




http://ecmarchitect.com/alfresco-developer-series-tutorials/workflow/tutorial/tutorial.html


http://docs.alfresco.com/activiti/topics/taskListeners.html

http://alfresco-gl.googlecode.com/svn/trunk/alfresco-3.4/20120125-alfresco.sql


http://svn.codehaus.org/activiti/projects/designer/trunk/org.activiti.designer.eclipse/src/main/resources/templates/parallel-review-group.bpmn20.xml


https://www.becpg.fr/redmine/projects/becpg-community/repository/revisions/445803e7606b/entry/becpg-plm/becpg-plm-core/src/main/config/workflow/quality/non-conformity-adhoc.bpmn



http://forums.activiti.org/content/cannot-find-scripting-engine-groovy

http://activiti.org/javadocs/org/activiti/engine/task/package-summary.html

http://svn.codehaus.org/activiti/activiti/branches/ACT-620/modules/activiti-engine/src/main/java/org/activiti/engine/impl/bpmn/behavior/UserTaskActivityBehavior.java