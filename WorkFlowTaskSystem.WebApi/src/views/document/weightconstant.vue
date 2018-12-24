<template>
    <div>
        <Card>
            <p slot="title">{{'数据管理'|l}}</p>
            <Dropdown slot="extra"  @on-click="handleClickActionsDropdown">
                <a href="javascript:void(0)">
                    {{'操作'|l}}
                    <Icon type="android-more-vertical"></Icon>
                </a>
                <DropdownMenu slot="list">
                        
                    <DropdownItem name='Refresh'>{{'刷新' | l}}</DropdownItem>
                    <DropdownItem name='Create' v-if="persBtn.create" >{{'添加' | l}}</DropdownItem>
                    <DropdownItem name='DeleteAll' v-if="persBtn.create" >{{'删除所有' | l}}</DropdownItem>
                </DropdownMenu>
            </Dropdown>
            
            <Table  :columns="columns"  border :data="weightconstants"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage" show-sizer show-elevator show-total ></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                    <Form ref="newweightconstantForm" :label-width="100" :rules="newweightconstantRule" :model="editweightconstant">
                            
                             <FormItem :label="L('宽度之和')" prop="weightDecimal" >
                                <Input v-model="editweightconstant.weightDecimal" number ></Input>
                            </FormItem>
                            <FormItem :label="L('重量限值')" prop="weightLimit" >
                                <Input v-model="editweightconstant.weightLimit" number ></Input>
                            </FormItem>  
                           
                                                          
                </Form>                        
            </div>
            <div slot="footer">
                <Button @click="cancel">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                <Form ref="weightconstantForm" :label-width="100" :rules="weightconstantRule" :model="editweightconstant">
                            <FormItem :label="L('宽度之和')" prop="weightDecimal" >
                                <Input v-model="editweightconstant.weightDecimal" number ></Input>
                            </FormItem>
                            <FormItem :label="L('重量限值')" prop="weightLimit" >
                                <Input v-model="editweightconstant.weightLimit" number ></Input>
                            </FormItem>  
                            
                                           
                </Form>
            </div>
            <div slot="footer">
                <Button @click="cancel">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
    </div>
</template>
<script>
export default {
    methods:{
        create(){
            this.editweightconstant={};  
            this.editweightconstant.description=abp.randomNumber();
            this.showModal=true;
        },
        cancel(){
            if(!!this.editweightconstant.id){
                this.showEditModal=false;
                this.getpage();
            }else{
                this.showModal=false;
                this.getpage();
            }
        },
        async save(){
            if(!!this.editweightconstant.id){
                this.$refs.weightconstantForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'weightconstant/update',
                            data:this.editweightconstant
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newweightconstantForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'weightconstant/create',
                            data:this.editweightconstant
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        pageChange(page){
            this.$store.commit('weightconstant/setCurrentPage',page);
            this.getpage();
        },
        pagesizeChange(pagesize){
            this.$store.commit('weightconstant/setPageSize',pagesize);
            this.getpage();
        },
        async getpage(){
            await this.$store.dispatch({
                type:'weightconstant/getAll'
            })
        },
        seachKeyChange(){
            
            this.getpage();
        },
        onselectionchange(row){
            this.selectedData=row;
        },
        handleClickActionsDropdown(name){
            if(name==='Create'){
                this.create();
            }else if(name==='Refresh'){
                this.getpage();
                this.$Message.success('刷新成功！');

            }else if(name=='DeleteAll'){
                var $this=this;
                this.$store.dispatch({
                    type:'weightconstant/deleteAll'
                }).then(function (response) {
                    $this.getpage();
                    $this.$Message.success('刷新成功！');
                }).catch(function (error) {
                    console.log(error);
                    
                });
                
            }
        }
    },
    data(){
        
        return{
            editweightconstant:{},
             persBtn:{
                create:true,//abp.auth.isGranted('Pages.weightconstants.Create'),
                update:true,//abp.auth.isGranted('Pages.weightconstants.Update'),
                delete:true,//abp.auth.isGranted('Pages.weightconstants.Delete'),
            },
            showModal:false,
            showEditModal:false,
            selectedData:[],
            seachKey:"",
            newweightconstantRule:{
                weightDecimal:[{validator:abp.validateInteger,trigger: 'blur'}],
                weightLimit:[{validator:abp.validateInteger,trigger: 'blur'}],
                
            },            
            weightconstantRule:{
               weightDecimal:[{validator:abp.validateInteger,trigger: 'blur'}],
                weightLimit:[{validator:abp.validateInteger,trigger: 'blur'}],
            },
            columns:[
            
                    {
                    title:this.L('序号'),
                        type: 'index',
                        width: 61,
                        align: 'center'
                    }
            ,{
                title:this.L('宽度之和'),
                 width: 160,
                key:'weightDecimal'
            },{
                title:this.L('重量限值'),
                key:'weightLimit'
            },{
                title: this.L('操作'),
                key: 'action',
                width:150,
                render:(h,params)=>{
                    var btns=[];
                            if(this.persBtn.update){
                                var d=h('Button',{
                                            props:{
                                                type:'primary',
                                                size:'small'
                                            },
                                            style:{
                                                marginRight:'5px'
                                            },
                                            on:{
                                                click:()=>{
                                                    this.editweightconstant=this.weightconstants[params.index];
                                                    this.showEditModal=true;
                                                }
                                            }
                                        },this.L('编辑'));
                                btns.push(d);
                            }
                           if(this.persBtn.delete){
                                   var d1=h('Button',{
                                            props:{
                                                type:'error',
                                                size:'small'
                                            },
                                            on:{
                                                click:async()=>{
                                                    this.$Modal.confirm({
                                                        title:this.L(''),
                                                        content:this.L('删除这个数据吗？'),
                                                        okText:this.L('是'),
                                                        cancelText:this.L('否'),
                                                        onOk:async()=>{
                                                            await this.$store.dispatch({
                                                                type:'weightconstant/delete',
                                                                data:this.weightconstants[params.index]
                                                            })
                                                            this.$Message.success('删除成功！');
                                                            await this.getpage();
                                                        }
                                                    })
                                                }
                                            }
                                        },this.L('删除'));
                                   btns.push(d1); 
                            }
                            return h('div',btns);                   
                }
            }]
        }
    },
    computed:{
        weightconstants(){
            return this.$store.state.weightconstant.weightconstants;
        },
        
        totalCount(){
            return this.$store.state.weightconstant.totalCount;
        },
        currentPage(){
            return this.$store.state.weightconstant.currentPage;
        },
        pageSize(){
            return this.$store.state.weightconstant.pageSize;
        },
        
    },
    async created(){
        this.getpage();
        
    }
}
</script>



