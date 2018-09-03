<template>
<div>
		<Form ref="newRoleForm" label-position="top" :rules="newRoleRule" :model="editRole">
                            <FormItem :label="L('角色名称')" prop="name">
                                <Input v-model="editRole.name" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('角色编码')" prop="code">
                                <Input v-model="editRole.code" :maxlength="32" :minlength="2"></Input>
                            </FormItem>
                            <FormItem :label="L('描述')" prop="description">
                                <Input v-model="editRole.description"></Input>
                            </FormItem>
                                              
        </Form>
</div>
</template>
<script>
export default {
    methods:{
        
    },
    data(){
        return{
            editRole:{},
             
            newRoleRule:{
                name:[{required:true,message:'Name is required',trigger: 'blur'}],
                code:[{required:true,message:'Code is required',trigger: 'blur'}],
            },            
        }
    },
    computed:{
        
    }
}
</script>